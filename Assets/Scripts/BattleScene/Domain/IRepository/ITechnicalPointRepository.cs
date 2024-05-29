using BattleScene.Domain.Aggregate;

namespace BattleScene.Domain.IRepository
{
    public interface ITechnicalPointRepository
    {
        public TechnicalPointAggregate Select();
        public void Update(TechnicalPointAggregate technicalPoint);
    }
}