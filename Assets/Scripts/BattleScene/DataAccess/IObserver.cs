namespace BattleScene.DataAccess
{
    public interface IObserver<in TItem>
    {
        public void Observe(TItem item);
    }
}