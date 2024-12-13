namespace BattleScene.DataAccesses
{
    public interface IUnique<out TId>
    {
        public TId Key { get; }
    }
}