namespace BattleScene.Framework.ViewModel
{
    public record MessageViewDto(
        string Message,
        bool NoWait = false);
}