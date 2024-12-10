using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Entity;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using Utility;

namespace BattleScene.UseCases.Service
{
    public class ResetService : ISkillElementService<RecoveryValueObject>
    {
        private readonly IAilmentResetService _ailmentReset;

        public ResetService(
            IAilmentResetService ailmentReset)
        {
            _ailmentReset = ailmentReset;
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
            // var actor = resetEvent.Actor ?? throw new InvalidOperationException();
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
            // TODO: 部位破壊を回復する処理を書くこと。
            var slipPartLookup = resetEventList
                .SelectMany(
                    collectionSelector: static resetEvent => resetEvent.TargetList,
                    resultSelector: static (resetEvent, target) => (resetEvent, target))
                .SelectMany(
                    collectionSelector: static x => x.resetEvent.ResetSlipCodeList,
                    resultSelector: static (x, resetSlipCode) => (targetId: x.target.Id, resetSlipCode))
                .ToLookup(static x => x.targetId, y => y.resetSlipCode);
            // TODO: スリップを回復する処理を書くこと。
        }
    }
}