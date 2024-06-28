namespace BattleScene.InterfaceAdapter.DataAccess.Factory.Dto
{
    public interface IListScriptableObjectItem<out TId>
    {
        public TId Id { get; }
    }
}