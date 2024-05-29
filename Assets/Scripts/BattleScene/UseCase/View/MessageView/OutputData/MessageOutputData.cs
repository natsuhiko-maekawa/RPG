namespace BattleScene.UseCase.View.MessageView.OutputData
{
    public record MessageOutputData(
        string Message,
        bool NoWait = false);
}