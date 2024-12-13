using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Entities;
using BattleScene.Domain.ValueObjects;
using BattleScene.UseCases.IServices;
using Utility;

namespace BattleScene.UseCases.Services
{
    public class ResetService : ISkillElementService<RecoveryValueObject>
    {
        private readonly IAilmentResetService _ailmentReset;
        private readonly IDestroyResetService _destroyReset;
        private readonly ISlipResetService _slipReset;

        public ResetService(
            IAilmentResetService ailmentReset,
            IDestroyResetService destroyReset,
            ISlipResetService slipReset)
        {
            _ailmentReset = ailmentReset;
            _destroyReset = destroyReset;
            _slipReset = slipReset;
        }

        public void UpdateBattleEvent(
            IReadOnlyList<BattleEventEntity> resetEventList,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<RecoveryValueObject> resetList,
            IReadOnlyList<CharacterEntity> targetList)
        {
            MyDebug.Assert(resetList.Count == 1);
            MyDebug.Assert(resetEventList.Count == 1);
            var resetEvent = resetEventList.Single();
            var reset = resetList.Single();
            resetEvent.UpdateReset(
                resetAilmentCodeList: reset.AilmentCodeList,
                resetBodyPartCodeList: reset.BodyPartCodeList,
                resetSlipCodeList: reset.SlipCodeList,
                targetList: targetList);
        }

        public void ExecuteBattleEvent(IReadOnlyList<BattleEventEntity> resetEventList)
        {
            var ailmentLookup = resetEventList
                .SelectMany(
                    collectionSelector: static resetEvent => resetEvent.TargetList,
                    resultSelector: static (resetEvent, target) => (resetEvent, target))
                .SelectMany(
                    collectionSelector: static x => x.resetEvent.ResetAilmentCodeList,
                    resultSelector: static (x, resetAilmentCode) => (targetId: x.target.Id, resetAilmentCode))
                .ToLookup(static x => x.targetId, y => y.resetAilmentCode);
            _ailmentReset.Reset(ailmentLookup);
            var destroyedPartLookup = resetEventList
                .SelectMany(
                    collectionSelector: static resetEvent => resetEvent.TargetList,
                    resultSelector: static (resetEvent, target) => (resetEvent, target))
                .SelectMany(
                    collectionSelector: static x => x.resetEvent.ResetBodyPartCodeList,
                    resultSelector: static (x, resetBodyPartCode) => (targetId: x.target.Id, resetBodyPartCode))
                .ToLookup(static x => x.targetId, y => y.resetBodyPartCode);
            _destroyReset.Reset(destroyedPartLookup);
            var slipPartLookup = resetEventList
                .SelectMany(
                    collectionSelector: static resetEvent => resetEvent.TargetList,
                    resultSelector: static (resetEvent, target) => (resetEvent, target))
                .SelectMany(
                    collectionSelector: static x => x.resetEvent.ResetSlipCodeList,
                    resultSelector: static (x, resetSlipCode) => (targetId: x.target.Id, resetSlipCode))
                .ToLookup(static x => x.targetId, y => y.resetSlipCode);
            _slipReset.Reset(slipPartLookup);
        }
    }
}