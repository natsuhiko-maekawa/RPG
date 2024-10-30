using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class ResetService : IPrimeSkillService<ResetParameterValueObject>
    {
        private readonly IAilmentResetService _ailmentReset;

        public ResetService(
            IAilmentResetService ailmentReset)
        {
            _ailmentReset = ailmentReset;
        }

        public IReadOnlyList<BattleEventValueObject> Generate(
            CharacterId actorId,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<ResetParameterValueObject> resetParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            if (resetParameterList.Count != 1)
                throw new InvalidOperationException(ExceptionMessage.ResetParameterIsNoSingle);
            var resetParameter = resetParameterList.Single();
            var reset = BattleEventValueObject.CreateReset(
                actorId: actorId,
                skillCode: skillCommon.SkillCode,
                targetIdList: targetIdList,
                ailmentCodeList: resetParameter.AilmentCodeList,
                slipCodeList: resetParameter.SlipCodeList,
                bodyPartCodeList: resetParameter.BodyPartCodeList);
            var resetArray = new[] { reset };
            return resetArray;
        }

        public void Register(IReadOnlyList<BattleEventValueObject> primeSkillList)
        {
            var ailmentLookup = primeSkillList
                .SelectMany(battleEvent => battleEvent.TargetIdList
                    .SelectMany(characterId => battleEvent.ResetAilmentCodeList
                        .Select(ailmentCode => (characterId, ailmentCode))))
                .ToLookup(x => x.characterId, y => y.ailmentCode);

            _ailmentReset.Reset(ailmentLookup);
        }
    }
}