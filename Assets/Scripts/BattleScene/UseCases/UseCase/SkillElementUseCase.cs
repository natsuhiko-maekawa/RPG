using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using BattleScene.UseCases.Service;

namespace BattleScene.UseCases.UseCase
{
    public class
        SkillElementUseCase<TSkillElement>
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

        [Obsolete]
        public IReadOnlyList<BattleEventEntity> GetBattleEventList(
            CharacterId actorId,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<TSkillElement> skillElementList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            throw new NotImplementedException();
            // var battleEventList = _skillElement.GenerateBattleEvent(
            //     actorId: actorId,
            //     skillCommon: skillCommon,
            //     primeSkillParameterList: skillElementList,
            //     targetIdList: targetIdList);
            // return battleEventList;
        }

        public IReadOnlyList<BattleEventEntity> ExecuteBattleEvent(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<TSkillElement> skillElementList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            var skillEvent = _battleLogger.GetLast();

            var battleEventList = new BattleEventEntity[skillElementList.Count];
            battleEventList[0] = skillEvent;
            if (skillElementList.Count > 0)
            {
                for (var i = 1; i < skillElementList.Count; ++i)
                {
                    var battleEvent = new BattleEventEntity(
                        battleEventId: new BattleEventId(),
                        sequence: skillEvent.Sequence + i,
                        turn: skillEvent.Turn,
                        actorId: skillEvent.ActorId);
                    _battleLogger.Log(battleEvent);
                    battleEventList[i] = battleEvent;
                }
            }

            _skillElement.UpdateBattleEvent(
                buffEventList: battleEventList, 
                skillCommon: skillCommon,
                skillElementList: skillElementList,
                targetIdList: targetIdList);

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