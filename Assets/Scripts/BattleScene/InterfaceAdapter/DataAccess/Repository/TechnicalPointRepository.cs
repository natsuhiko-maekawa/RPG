using BattleScene.Domain.Aggregate;
using BattleScene.Domain.IRepository;

namespace BattleScene.InterfaceAdapter.DataAccess.Repository
{
    public class TechnicalPointRepository : ITechnicalPointRepository
    {
        public TechnicalPointAggregate Select()
        {
            throw new System.NotImplementedException();
        }

        public void Update(TechnicalPointAggregate technicalPoint)
        {
            throw new System.NotImplementedException();
        }
    }
}