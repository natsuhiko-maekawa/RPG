namespace BattleScene.Framework.ViewModels
{
    public record OrderViewModel(
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