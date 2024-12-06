using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using Utility;

namespace BattleScene.UseCases.Service
{
    public class DestroyService : ISkillElementService<DestroyValueObject>
    {
        private readonly IActualTargetIdPickerService _actualTargetIdPicker;
        private readonly IRepository<BodyPartEntity, (CharacterId, BodyPartCode)> _bodyPartRepository;

        public DestroyService(
            IActualTargetIdPickerService actualTargetIdPicker,
            IRepository<BodyPartEntity, (CharacterId, BodyPartCode)> bodyPartRepository)
        {
            _actualTargetIdPicker = actualTargetIdPicker;
            _bodyPartRepository = bodyPartRepository;
        }

        [Obsolete]
        public IReadOnlyList<BattleEventValueObject> GenerateBattleEvent(
            CharacterId actorId,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<DestroyValueObject> destroyedParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            throw new NotImplementedException();
            // var destroyList = destroyedParameterList.Select(GetDestroy).ToList();
            // return destroyList;
            //
            // BattleEventValueObject GetDestroy(DestroyValueObject destroyedParameter)
            // {
            //     var actualTargetIdList = _actualTargetIdPicker.Pick(
            //         actorId: actorId,
            //         targetIdList: targetIdList,
            //         luckRate: destroyedParameter.LuckRate);
            //
            //     var destroy = BattleEventValueObject.CreateDestroy(
            //         actorId: actorId,
            //         targetIdList: targetIdList,
            //         actualTargetIdList: actualTargetIdList,
            //         skillCode: skillCommon.SkillCode,
            //         bodyPartCode: destroyedParameter.BodyPartCode,
            //         destroyCount: destroyedParameter.Count);
            //     return destroy;
            // }
        }

        public void UpdateBattleEvent(
            IReadOnlyList<BattleEventEntity> destroyEventList,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<DestroyValueObject> destroyList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            MyDebug.Assert(destroyEventList.Count == destroyList.Count);
            foreach (var (battleEvent, destroy) in destroyEventList
                         .Zip(destroyList, (battleEvent, destroy) => (battleEvent, destroy)))
            {
                battleEvent.UpdateDestroy(
                    destroyedPart: destroy.BodyPartCode,
                    destroyCount: destroy.Count,
                    targetIdList: targetIdList);
            }
        }

        public void ExecuteBattleEvent(IReadOnlyList<BattleEventEntity> destroyEventList)
        {
            foreach (var destroyEvent in destroyEventList)
            {
                foreach (var characterId in destroyEvent.ActualTargetIdList)
                {
                    var bodyPart = _bodyPartRepository.Get((characterId, destroyEvent.DestroyedPart));
                    bodyPart.Destroyed();
                }
            }
        }
    }
}