using System;
using BattleScene.UseCase.View.MessageView.OutputData;

namespace BattleScene.UseCase.View.MessageView.OutputBoundary
{
    public interface IMessageViewPresenter
    {
        [Obsolete]
        public void StartMessageView(string message, bool noWait = false);

        public void Start(MessageOutputData outputData);
    }
}