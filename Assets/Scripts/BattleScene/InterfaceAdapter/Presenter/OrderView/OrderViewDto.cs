namespace BattleScene.InterfaceAdapter.Presenter.OrderView
{
    public record OrderViewDto(
        ItemType ItemType,
        string EnemyName,
        int? AilmentsInt);

    public enum ItemType
    {
        Player,
        Enemy,
        Ailments
    }
}