using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class CureService : IPrimeSkillService<CureValueObject>
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

        public IReadOnlyList<BattleEventValueObject> Generate(
            CharacterId actorId,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<CureValueObject> cureParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            if (cureParameterList.Count != 1)
                throw new InvalidOperationException(ExceptionMessage.ResetParameterIsNoSingle);
            var cureParameter = cureParameterList.Single();
            var cureAmount = _cureEvaluator.Evaluate(actorId, cureParameter);
            var curingList = targetIdList
                .Select(targetId => new CuringValueObject(
                    Amount: cureAmount,
                    TargetId: targetId))
                .ToList();

            var cure = BattleEventValueObject.CreateCure(
                skillCode: skillCommon.SkillCode,
                actorId: actorId,
                curingList: curingList);
            var cureArray = new[] { cure };
            return cureArray;
        }

        public void Register(IReadOnlyList<BattleEventValueObject> cureList)
        {
            _hitPoint.Cure(cureList);
        }
    }
}