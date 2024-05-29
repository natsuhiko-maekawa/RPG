using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.IRepository
{
    public interface ISelectorRepository
    {
        public SelectorAggregate Select(SelectorId id);
        public void Update(SelectorAggregate selector);
    }
}