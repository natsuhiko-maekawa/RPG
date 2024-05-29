using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.IRepository
{
    public interface ISkillSelectorRepository
    {
        public SkillSelectorAggregate Select(SkillSelectorId id);
        public void Update(SkillSelectorAggregate skillSelectorAggregate);
    }
}