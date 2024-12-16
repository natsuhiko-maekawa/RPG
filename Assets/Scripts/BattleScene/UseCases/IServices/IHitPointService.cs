using System.Collections.Generic;
using BattleScene.Domain.Entities;

namespace BattleScene.UseCases.IServices
{
    public interface IHitPointService
    {
        public void Damaged(BattleEventEntity damageEvent);
        public void Damaged(IReadOnlyList<BattleEventEntity> damageEventList);
        public void Cure(IReadOnlyList<BattleEventEntity> cureEventList);
    }
}