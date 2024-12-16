using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.Ids;
using BattleScene.Domain.ValueObjects;
using BattleScene.UseCases.IServices;
using BattleScene.UseCases.Services;

namespace BattleScene.UseCases.UseCases
{
    public class SkillUseCase<TSkillComponent>
    {
        private readonly ISkillService<TSkillComponent> _skill;
        private readonly BattleLoggerService _battleLogger;
        private readonly IRepository<BattleEventEntity, BattleEventId> _battleLogRepository;

        public SkillUseCase(
            ISkillService<TSkillComponent> skill,
            BattleLoggerService battleLogger,
            IRepository<BattleEventEntity, BattleEventId> battleLogRepository)
        {
            _skill = skill;
            _battleLogger = battleLogger;
            _battleLogRepository = battleLogRepository;
        }

        public IReadOnlyList<BattleEventEntity> ExecuteBattleEvent(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<TSkillComponent> skillComponentList,
            IReadOnlyList<CharacterEntity> targetList)
        {
            var battleEventList = new BattleEventEntity[skillComponentList.Count];
            var skillEvent = _battleLogger.GetLast();
            var i = 0;
            if (_battleLogger.IsSingleAsTurn(skillEvent))
            {
                battleEventList[0] = skillEvent;
                i = 1;
            }

            for (; i < skillComponentList.Count; ++i)
            {
                var battleEvent = new BattleEventEntity(
                    battleEventId: new BattleEventId(),
                    sequence: skillEvent.Sequence + i,
                    turn: skillEvent.Turn,
                    actor: skillEvent.Actor);
                _battleLogger.Log(battleEvent);
                battleEventList[i] = battleEvent;
            }

            _skill.UpdateBattleEvent(
                battleEventList: battleEventList,
                skillCommon: skillCommon,
                skillComponentList: skillComponentList,
                targetList: targetList);

            _skill.ExecuteBattleEvent(battleEventList);

            return battleEventList;
        }

        public bool IsExecutedDamage()
        {
            var value = _battleLogRepository.Get()
                .Where(x => x.Turn == _battleLogRepository.Get().Max().Turn)
                .Any(x => x.AttackList.Count != 0);
            return value;
        }
    }
}