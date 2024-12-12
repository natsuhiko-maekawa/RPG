namespace BattleScene.Framework.ViewModels
{
    public record BuffViewModel(
        int BuffId,
        BuffState BuffState);

    public enum BuffState : sbyte
    {
        NoBuff = 0,
        Buff = 1,
        DeBuff = -1
    }
}