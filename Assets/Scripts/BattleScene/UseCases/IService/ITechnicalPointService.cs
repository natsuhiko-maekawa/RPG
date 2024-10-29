using System.Collections.Generic;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.IService
{
    public interface ITechnicalPointService
    {
        public int Get();
        public void Reduce(SkillValueObject skill);
        public void Restore(IReadOnlyList<BattleEventValueObject> restoreList);
    }
}