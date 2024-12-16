namespace BattleScene.Views.ViewModels
{
    public record OrderViewModel(
        ItemType ItemType,
        string EnemyImagePath = "",
        int AilmentInt = 0);

    public enum ItemType
    {
        Player,
        Enemy,
        Ailment
    }
}