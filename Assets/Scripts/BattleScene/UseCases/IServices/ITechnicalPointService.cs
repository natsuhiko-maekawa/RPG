using System.Collections.Generic;
using BattleScene.Domain.Entities;
using BattleScene.Domain.ValueObjects;

namespace BattleScene.UseCases.IServices
{
    public interface ITechnicalPointService
    {
        public int Get();
        public void Reduce(SkillValueObject skill);
        public void Restore(IReadOnlyList<BattleEventEntity> restoreEventList);
    }
}