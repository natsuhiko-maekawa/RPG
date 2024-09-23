﻿using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using UnityEngine;

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

        public IReadOnlyList<SlipValueObject> Generate(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<SlipParameterValueObject> slipParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            var value = _orderedItems.First().TryGetCharacterId(out var actorId);
            Debug.Assert(value);

            var slipList = slipParameterList
                .Select(GetSlip)
                .ToList();
            return slipList;

            SlipValueObject GetSlip(SlipParameterValueObject slipParameter)
            {
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
}