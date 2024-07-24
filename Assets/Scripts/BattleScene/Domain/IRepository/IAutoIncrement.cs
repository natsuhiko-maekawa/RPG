using BattleScene.Domain.OldId;

namespace BattleScene.Domain.IRepository
{
    public interface IAutoIncrement<out TId>
        where TId : Number<TId>, new()
    {
        public TId GenerateId();
    }
}