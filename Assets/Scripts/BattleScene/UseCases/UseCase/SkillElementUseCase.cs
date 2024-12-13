using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.Ids;
using BattleScene.Domain.ValueObjects;
using BattleScene.UseCases.IService;
using BattleScene.UseCases.Service;

namespace BattleScene.UseCases.UseCase
{
    public class SkillElementUseCase<TSkillElement>
    {
        private readonly ISkillElementService<TSkillElement> _skillElement;
        private readonly BattleLoggerService _battleLogger;
        private readonly IRepository<BattleEventEntity, BattleEventId> _battleLogRepository;

        public SkillElementUseCase(
            ISkillElementService<TSkillElement> skillElement,
            BattleLoggerService battleLogger,
            IRepository<BattleEventEntity, BattleEventId> battleLogRepository)
        {
            _skillElement = skillElement;
            _battleLogger = battleLogger;
            _battleLogRepository = battleLogRepository;
        }

        public IReadOnlyList<BattleEventEntity> ExecuteBattleEvent(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<TSkillElement> skillElementList,
            IReadOnlyList<CharacterEntity> targetList)
        {
            var battleEventList = new BattleEventEntity[skillElementList.Count];
            var skillEvent = _battleLogger.GetLast();
            var i = 0;
            if (_battleLogger.IsSingleAsTurn(skillEvent))
            {
                battleEventList[0] = skillEvent;
                i = 1;
            }

            for (; i < skillElementList.Count; ++i)
            {
                var battleEvent = new BattleEventEntity(
                    battleEventId: new BattleEventId(),
                    sequence: skillEvent.Sequence + i,
                    turn: skillEvent.Turn,
                    actor: skillEvent.Actor);
                _battleLogger.Log(battleEvent);
                battleEventList[i] = battleEvent;
            }

            _skillElement.UpdateBattleEvent(
                battleEventList: battleEventList, 
                skillCommon: skillCommon,
                skillElementList: skillElementList,
                targetList: targetList);

            _skillElement.ExecuteBattleEvent(battleEventList);

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