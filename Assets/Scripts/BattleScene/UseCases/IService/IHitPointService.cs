using System.Collections.Generic;
using BattleScene.Domain.Entity;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.IService
{
    public interface IHitPointService
    {
        public void Damaged(BattleEventEntity damageEvent);
        public void Damaged(IReadOnlyList<BattleEventEntity> damageEventList);
        public void Cure(IReadOnlyList<BattleEventEntity> cureEventList);
    }
}