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
        private readonly IRepository<BodyPartEntity, (CharacterId, BodyPartCode)> _bodyPartRepository;

        public DestroyService(
            IRepository<BodyPartEntity, (CharacterId, BodyPartCode)> bodyPartRepository)
        {
            _bodyPartRepository = bodyPartRepository;
        }

        public void UpdateBattleEvent(
            IReadOnlyList<BattleEventEntity> destroyEventList,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<DestroyValueObject> destroyList,
            IReadOnlyList<CharacterEntity> targetList)
        {
            MyDebug.Assert(destroyEventList.Count == destroyList.Count);
            foreach (var (battleEvent, destroy) in destroyEventList
                         .Zip(destroyList, (battleEvent, destroy) => (battleEvent, destroy)))
            {
                battleEvent.UpdateDestroy(
                    destroyedPart: destroy.BodyPartCode,
                    destroyCount: destroy.Count,
                    targetList: targetList);
            }
        }

        public void ExecuteBattleEvent(IReadOnlyList<BattleEventEntity> destroyEventList)
        {
            foreach (var destroyEvent in destroyEventList)
            {
                foreach (var target in destroyEvent.ActualTargetList)
                {
                    var bodyPart = _bodyPartRepository.Get((target.Id, destroyEvent.DestroyedPart));
                    bodyPart.Destroyed();
                }
            }
        }
    }
}