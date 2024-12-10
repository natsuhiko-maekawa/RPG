﻿using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using Utility;

namespace BattleScene.UseCases.Service
{
    public class SlipService : ISkillElementService<SlipValueObject>
    {
        private readonly IActualTargetIdPickerService _actualTargetIdPicker;
        private readonly IRepository<SlipEntity, SlipCode> _slipRepository;

        public SlipService(
            IActualTargetIdPickerService actualTargetIdPicker,
            IRepository<SlipEntity, SlipCode> slipRepository)
        {
            _actualTargetIdPicker = actualTargetIdPicker;
            _slipRepository = slipRepository;
        }

        public void UpdateBattleEvent(
            IReadOnlyList<BattleEventEntity> slipEventList,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<SlipValueObject> slipList,
            IReadOnlyList<CharacterEntity> targetList)
        {
            MyDebug.Assert(slipEventList.Count == slipList.Count);
            foreach (var (battleEvent, slip) in slipEventList
                         .Zip(slipList, (battleEvent, slip) => (battleEvent, slip)))
            {
                var actualTargetIdList = _actualTargetIdPicker.Pick(
                    actor: battleEvent.Actor!,
                    targetList: targetList,
                    luckRate: slip.LuckRate);

                battleEvent.UpdateSlip(
                    slipCode: slip.SlipCode,
                    targetList: targetList,
                    actualTargetList: actualTargetIdList);
            }
        }

        public void ExecuteBattleEvent(IReadOnlyList<BattleEventEntity> slipEventList)
        {
            foreach (var slipEvent in slipEventList)
            {
                if (slipEvent.ActualTargetList.Count != 0)
                {
                    _slipRepository.Get(slipEvent.SlipCode).Effects = true;
                }
            }
        }
    }
}