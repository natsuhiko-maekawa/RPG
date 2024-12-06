using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
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

        [Obsolete]
        public IReadOnlyList<BattleEventValueObject> GenerateBattleEvent(
            CharacterId actorId,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<CureValueObject> cureParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            throw new NotImplementedException();
            // if (cureParameterList.Count != 1)
            //     throw new InvalidOperationException(ExceptionMessage.ResetParameterIsNoSingle);
            // var cureParameter = cureParameterList.Single();
            // var cureAmount = _cureEvaluator.Evaluate(actorId, cureParameter);
            // var curingList = targetIdList
            //     .Select(targetId => new CuringValueObject(
            //         Amount: cureAmount,
            //         TargetId: targetId))
            //     .ToList();
            //
            // var cure = BattleEventValueObject.CreateCure(
            //     skillCode: skillCommon.SkillCode,
            //     actorId: actorId,
            //     curingList: curingList);
            // var cureArray = new[] { cure };
            // return cureArray;
        }

        public void UpdateBattleEvent(
            IReadOnlyList<BattleEventEntity> cureEventList,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<CureValueObject> cureList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            MyDebug.Assert(cureList.Count == 1);
            MyDebug.Assert(cureEventList.Count == 1);
            var cureEvent = cureEventList.Single();
            var actorId = cureEvent.ActorId;
            MyDebug.Assert(actorId is not null);
            var cure = cureList.Single();
            var cureAmount = _cureEvaluator.Evaluate(actorId!, cure);
            var curingList = targetIdList
                .Select(targetId => new CuringValueObject(
                    amount: cureAmount,
                    targetId: targetId))
                .ToArray();

            cureEvent.UpdateCure(
                    curingList: curingList,
                    targetIdList: targetIdList);
        }

        public void ExecuteBattleEvent(IReadOnlyList<BattleEventEntity> cureEventList)
        {
            _hitPoint.Cure(cureEventList);
        }
    }
}