using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.Ids;
using BattleScene.Domain.ValueObjects;
using BattleScene.UseCases.IServices;
using Utility;

namespace BattleScene.UseCases.Services
{
    public class AilmentService : ISkillService<AilmentValueObject>
    {
        private readonly IActualTargetIdPickerService _actualTargetIdPicker;
        private readonly IRepository<AilmentEntity, (CharacterId, AilmentCode)> _ailmentRepository;

        public AilmentService(
            IActualTargetIdPickerService actualTargetIdPicker,
            IRepository<AilmentEntity, (CharacterId, AilmentCode)> ailmentRepository)
        {
            _actualTargetIdPicker = actualTargetIdPicker;
            _ailmentRepository = ailmentRepository;
        }

        public void UpdateBattleEvent(
            IReadOnlyList<BattleEventEntity> ailmentEventList,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<AilmentValueObject> ailmentList,
            IReadOnlyList<CharacterEntity> targetList)
        {
            MyDebug.Assert(ailmentEventList.Count == ailmentList.Count);
            foreach (var (battleEvent, ailment) in ailmentEventList
                         .Zip(ailmentList, (battleEvent, ailment) => (battleEvent, ailment)))
            {
                var actor = battleEvent.Actor ?? throw new InvalidOperationException();
                var actualTargetIdList = _actualTargetIdPicker.Pick(
                    actor: actor,
                    targetList: targetList,
                    luckRate: ailment.LuckRate);

                battleEvent.UpdateAilment(
                    ailmentCode: ailment.AilmentCode,
                    targetList: targetList,
                    actualTargetList: actualTargetIdList);
            }
        }

        public void ExecuteBattleEvent(IReadOnlyList<BattleEventEntity> ailmentList)
        {
            foreach (var ailment in ailmentList)
            {
                foreach (var target in ailment.ActualTargetList)
                {
                    var ailment1 = _ailmentRepository.Get((target.Id, ailment.AilmentCode));
                    ailment1.Effects = true;
                }
            }
        }
    }
}