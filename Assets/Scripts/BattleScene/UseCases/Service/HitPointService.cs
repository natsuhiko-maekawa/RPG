using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Entities;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class HitPointService : IHitPointService
    {
        public void Damaged(BattleEventEntity damageEvent)
        {
            foreach (var (damagedCharacter, damageAmount) in damageEvent.AttackList
                         .Where(x => x.IsHit)
                         .GroupBy(x => x.Target)
                         .Select(x => x
                             .Select(y => (target: y.Target, amount: y.Amount))
                             .Aggregate((y, z) => (y.target, y.amount + z.amount)))
                         .ToDictionary(x => x.target, x => x.amount))
            {
                damagedCharacter.CurrentHitPoint -= damageAmount;
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
                curing.Target.CurrentHitPoint += curing.Amount;
            }
        }
    }
}