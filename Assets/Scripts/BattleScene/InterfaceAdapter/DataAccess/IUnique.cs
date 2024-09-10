namespace BattleScene.Domain.Interface
{
    public interface IUnique<out TId>
    {
        public TId Key { get; }
    }
}