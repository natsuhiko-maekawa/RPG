namespace BattleScene.DataAccess
{
    public interface IReactive<in TItem>
    {
        public void Observe(TItem item);
    }
}