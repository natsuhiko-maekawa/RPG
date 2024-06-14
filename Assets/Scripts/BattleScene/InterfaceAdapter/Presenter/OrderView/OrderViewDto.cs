namespace BattleScene.InterfaceAdapter.Presenter.OrderView
{
    public record OrderViewDto(
        ItemType ItemType,
        string EnemyImagePath = "",
        int? AilmentNumber = null);

    public enum ItemType
    {
        Player,
        Enemy,
        Ailment
    }
}