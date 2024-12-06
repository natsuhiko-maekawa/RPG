using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
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

        // public IReadOnlyList<BattleEventValueObject> GenerateBattleEvent(
        //     CharacterId actorId,
        //     SkillCommonValueObject skillCommon,
        //     IReadOnlyList<RecoveryValueObject> resetParameterList,
        //     IReadOnlyList<CharacterId> targetIdList)
        // {
        //     if (resetParameterList.Count != 1)
        //         throw new InvalidOperationException(ExceptionMessage.ResetParameterIsNoSingle);
        //     var resetParameter = resetParameterList.Single();
        //     var reset = BattleEventValueObject.CreateReset(
        //         actorId: actorId,
        //         skillCode: skillCommon.SkillCode,
        //         targetIdList: targetIdList,
        //         ailmentCodeList: resetParameter.AilmentCodeList,
        //         slipCodeList: resetParameter.SlipCodeList,
        //         bodyPartCodeList: resetParameter.BodyPartCodeList);
        //     var resetArray = new[] { reset };
        //     return resetArray;
        // }

        public void UpdateBattleEvent(
            IReadOnlyList<BattleEventEntity> resetEventList,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<RecoveryValueObject> resetList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            MyDebug.Assert(resetList.Count == 1);
            MyDebug.Assert(resetEventList.Count == 1);
            var resetEvent = resetEventList.Single();
            var actorId = resetEvent.ActorId;
            MyDebug.Assert(actorId is not null);
            var reset = resetList.Single();
            resetEvent.UpdateReset(
                resetAilmentCodeList: reset.AilmentCodeList,
                resetBodyPartCodeList: reset.BodyPartCodeList,
                resetSlipCodeList: reset.SlipCodeList,
                targetIdList: targetIdList);
        }

        public void ExecuteBattleEvent(IReadOnlyList<BattleEventEntity> resetEventList)
        {
            // var ailmentLookup = resetEventList
            //     .SelectMany(resetEvent => resetEvent.TargetIdList
            //         .SelectMany(characterId => resetEvent.ResetAilmentCodeList
            //             .Select(ailmentCode => (characterId, ailmentCode))))
            //     .ToLookup(x => x.characterId, y => y.ailmentCode);
            var ailmentLookup = resetEventList
                .SelectMany(
                    collectionSelector: static resetEvent => resetEvent.TargetIdList,
                    resultSelector: static (resetEvent, targetId) => (resetEvent, targetId))
                .SelectMany(
                    collectionSelector: static x => x.resetEvent.ResetAilmentCodeList,
                    resultSelector: static (x, resetAilmentCode) => (x.targetId, resetAilmentCode))
                .ToLookup(static x => x.targetId, y => y.resetAilmentCode);
            _ailmentReset.Reset(ailmentLookup);
            var destroyedPartLookup = resetEventList
                .SelectMany(
                    collectionSelector: static resetEvent => resetEvent.TargetIdList,
                    resultSelector: static (resetEvent, targetId) => (resetEvent, targetId))
                .SelectMany(
                    collectionSelector: static x => x.resetEvent.ResetBodyPartCodeList,
                    resultSelector: static (x, resetBodyPartCode) => (x.targetId, resetBodyPartCode))
                .ToLookup(static x => x.targetId, y => y.resetBodyPartCode);
            // TODO: 部位破壊を回復する処理を書く
            var slipPartLookup = resetEventList
                .SelectMany(
                    collectionSelector: static resetEvent => resetEvent.TargetIdList,
                    resultSelector: static (resetEvent, targetId) => (resetEvent, targetId))
                .SelectMany(
                    collectionSelector: static x => x.resetEvent.ResetSlipCodeList,
                    resultSelector: static (x, resetSlipCode) => (x.targetId, resetSlipCode))
                .ToLookup(static x => x.targetId, y => y.resetSlipCode);
            // TODO: スリップを回復する処理を書く
        }
    }
}