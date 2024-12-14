namespace BattleScene.DataAccesses
{
    public interface IUnique<out TId> where TId : notnull
    {
        public TId Key { get; }
    }
}