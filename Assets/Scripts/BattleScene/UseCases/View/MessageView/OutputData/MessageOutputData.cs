using BattleScene.Domain.Code;

namespace BattleScene.UseCases.View.MessageView.OutputData
{
    public record MessageOutputData(
        string Message,
        MessageCode MessageCode = MessageCode.NoMessage,
        bool NoWait = false);
}