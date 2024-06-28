namespace BattleScene.Framework.Resource
{
    public interface IListScriptableObjectItem<out TId>
    {
        public TId Id { get; }
    }
}