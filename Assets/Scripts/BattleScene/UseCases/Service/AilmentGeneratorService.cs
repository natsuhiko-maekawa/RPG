using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class AilmentGeneratorService : IPrimeSkillGeneratorService<AilmentParameterValueObject, AilmentValueObject>
    {
        private readonly ActualTargetIdPickerService _actualTargetIdPicker;
        private readonly OrderedItemsDomainService _orderedItems;

        public AilmentGeneratorService(
            ActualTargetIdPickerService actualTargetIdPicker,
            OrderedItemsDomainService orderedItems)
        {
            _actualTargetIdPicker = actualTargetIdPicker;
            _orderedItems = orderedItems;
        }

        public IReadOnlyList<AilmentValueObject> Generate(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<AilmentParameterValueObject> primeSkillParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            if (!_orderedItems.First().TryGetCharacterId(out var actorId)) throw new InvalidOperationException();
            
            var ailmentList = primeSkillParameterList.Select(GetAilment).ToList();
            
            return ailmentList;
            
            AilmentValueObject GetAilment(AilmentParameterValueObject ailmentParameter)
            {
                var actualTargetIdList = _actualTargetIdPicker.Pick(
                    targetIdList: targetIdList,
                    luckRate: ailmentParameter.LuckRate);
            
                var ailment = new AilmentValueObject(
                    actorId: actorId,
                    skillCode: skillCommon.SkillCode,
                    ailmentCode: ailmentParameter.AilmentCode,
                    targetIdList: targetIdList,
                    actualTargetIdList: actualTargetIdList);

                return ailment;
            }
        }
    }
}