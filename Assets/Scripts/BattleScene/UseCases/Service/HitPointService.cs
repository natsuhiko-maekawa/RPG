using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class HitPointService : IHitPointService
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;

        public HitPointService(
            IRepository<CharacterEntity, CharacterId> characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public void Damaged(BattleEventEntity damageEvent)
        {
            foreach (var (damagedCharacterId, damageAmount) in damageEvent.AttackList
                         .Where(x => x.IsHit)
                         .GroupBy(x => x.TargetId)
                         .Select(x => x
                             .Select(y => (targetId: y.TargetId, amount: y.Amount))
                             .Aggregate((y, z) => (y.targetId, y.amount + z.amount)))
                         .ToDictionary(x => x.targetId, x => x.amount))
            {
                var character = _characterRepository.Get(damagedCharacterId);
                character.CurrentHitPoint -= damageAmount;
            }
        }

        public void Damaged(IReadOnlyList<BattleEventEntity> damageEventList)
        {
            foreach (var damageEvent in damageEventList) Damaged(damageEvent);
        }

        public void Cure(IReadOnlyList<BattleEventEntity> cureEventList)
        {
            foreach (var curing in cureEventList.SelectMany(x => x.CuringList))
            {
                var character = _characterRepository.Get(curing.TargetId);
                character.CurrentHitPoint += curing.Amount;
            }
        }
    }
}