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
    public class AilmentService : ISkillElementService<AilmentValueObject>
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
            IReadOnlyList<CharacterId> targetIdList)
        {
            MyDebug.Assert(ailmentEventList.Count == ailmentList.Count);
            foreach (var (battleEvent, ailment) in ailmentEventList
                         .Zip(ailmentList, (battleEvent, ailment) => (battleEvent, ailment)))
            {
                var actualTargetIdList = _actualTargetIdPicker.Pick(
                    actorId: battleEvent.ActorId!,
                    targetIdList: targetIdList,
                    luckRate: ailment.LuckRate);

                battleEvent.UpdateAilment(
                    ailmentCode: ailment.AilmentCode,
                    targetIdList: targetIdList,
                    actualTargetIdList: actualTargetIdList);
            }
        }

        public void ExecuteBattleEvent(IReadOnlyList<BattleEventEntity> ailmentList)
        {
            foreach (var ailment in ailmentList)
            {
                foreach (var characterId in ailment.ActualTargetIdList)
                {
                    var ailment1 = _ailmentRepository.Get((characterId, ailment.AilmentCode));
                    ailment1.Effects = true;
                }
            }
        }
    }
}