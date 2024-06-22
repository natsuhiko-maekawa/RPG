using BattleScene.Domain.Code;

namespace BattleScene.UseCase.View.MessageView.OutputData
{
    public record MessageOutputData(
        string Message,
        MessageCode MessageCode = MessageCode.NoMessage,
        bool NoWait = false);
}