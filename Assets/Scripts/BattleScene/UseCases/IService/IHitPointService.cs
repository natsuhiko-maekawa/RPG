using System.Collections.Generic;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.IService
{
    public interface IHitPointService
    {
        public void Damaged(BattleEventValueObject damageEvent);
        public void Damaged(IReadOnlyList<BattleEventValueObject> damageEventList);
        public void Cure(IReadOnlyList<BattleEventValueObject> cureEventList);
    }
}