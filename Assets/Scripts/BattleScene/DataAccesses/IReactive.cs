namespace BattleScene.DataAccesses
{
    public interface IReactive<in TItem>
    {
        public void Observe(TItem item);
    }
}