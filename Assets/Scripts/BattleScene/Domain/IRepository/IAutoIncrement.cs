using BattleScene.Domain.Id;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.IRepository
{
    public interface IAutoIncrement<TId>
        where TId : IId
    {
        public AutoIncrementId<TId> GenerateId();
    }
}