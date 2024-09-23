using System;
using System.Collections.Generic;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Service
{
    public class SlipGeneratorService
    {
        private readonly ActualTargetIdPickerService _actualTargetIdPicker;
        private readonly OrderedItemsDomainService _orderedItems;

        public SlipGeneratorService(
            ActualTargetIdPickerService actualTargetIdPicker,
            OrderedItemsDomainService orderedItems)
        {
            _actualTargetIdPicker = actualTargetIdPicker;
            _orderedItems = orderedItems;
        }

        public SlipValueObject Generate(
            SkillCommonValueObject skillCommon,
            SlipParameterValueObject slipParameter,
            IReadOnlyList<CharacterId> targetIdList)
        {
            if (!_orderedItems.First().TryGetCharacterId(out var actorId)) throw new InvalidOperationException();
            var actualTargetIdList = _actualTargetIdPicker.Pick(
                targetIdList: targetIdList, 
                luckRate: slipParameter.LuckRate);
            
            var slip = new SlipValueObject(
                ActorId: actorId,
                SkillCode: skillCommon.SkillCode,
                SlipDamageCode: slipParameter.SlipDamageCode,
                TargetIdList: targetIdList,
                ActualTargetIdList: actualTargetIdList);
            return slip;
        }
    }
}