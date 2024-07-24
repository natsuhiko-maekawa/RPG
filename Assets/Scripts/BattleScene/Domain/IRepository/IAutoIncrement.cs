using BattleScene.Domain.Id;

namespace BattleScene.Domain.IRepository
{
    public interface IAutoIncrement<out TId>
        where TId : Number<TId>, new()
    {
        public TId GenerateId();
    }
}