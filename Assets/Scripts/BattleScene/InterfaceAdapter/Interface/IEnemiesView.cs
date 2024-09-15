namespace BattleScene.InterfaceAdapter.Interface
{
    public interface IEnemiesView
    {
        public void StopEnemyFrameView();
        public IEnemyView this[int i] { get; }
    }
}