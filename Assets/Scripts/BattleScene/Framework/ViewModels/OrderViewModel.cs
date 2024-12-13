namespace BattleScene.Framework.ViewModels
{
    public record OrderViewModel(
        ItemType ItemType,
        string EnemyImagePath = "",
        int AilmentNumber = 0);

    public enum ItemType
    {
        Player,
        Enemy,
        Ailment
    }
}