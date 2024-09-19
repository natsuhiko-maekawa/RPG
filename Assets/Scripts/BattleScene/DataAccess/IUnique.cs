namespace BattleScene.DataAccess
{
    public interface IUnique<out TId>
    {
        public TId Key { get; }
    }
}