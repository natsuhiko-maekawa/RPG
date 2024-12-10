using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Entity;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using Utility;

namespace BattleScene.UseCases.Service
{
    public class CureService : ISkillElementService<CureValueObject>
    {
        private readonly CureEvaluatorService _cureEvaluator;
        private readonly IHitPointService _hitPoint;

        public CureService(
            CureEvaluatorService cureEvaluator,
            IHitPointService hitPoint)
        {
            _cureEvaluator = cureEvaluator;
            _hitPoint = hitPoint;
        }

        public void UpdateBattleEvent(
            IReadOnlyList<BattleEventEntity> cureEventList,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<CureValueObject> cureList,
            IReadOnlyList<CharacterEntity> targetList)
        {
            MyDebug.Assert(cureList.Count == 1);
            MyDebug.Assert(cureEventList.Count == 1);
            var cureEvent = cureEventList.Single();
            var actor = cureEvent.Actor ?? throw new InvalidOperationException();
            var cure = cureList.Single();
            var cureAmount = _cureEvaluator.Evaluate(actor, cure);
            var curingList = targetList
                .Select(targetId => new CuringValueObject(
                    amount: cureAmount,
                    target: targetId))
                .ToArray();

            cureEvent.UpdateCure(
                    curingList: curingList,
                    targetList: targetList);
        }

        public void ExecuteBattleEvent(IReadOnlyList<BattleEventEntity> cureEventList)
        {
            _hitPoint.Cure(cureEventList);
        }
    }
}