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