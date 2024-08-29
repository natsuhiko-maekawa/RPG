using System;
using BattleScene.Domain.Code;
using BattleScene.UseCases.View.MessageView.OutputData;

namespace BattleScene.UseCases.View.MessageView.OutputBoundary
{
    public interface IMessageViewPresenter
    {
        [Obsolete]
        public void StartMessageView(string message, bool noWait = false);

        public void Start(MessageCode messageCode, bool noWait = false);
        public void Start(MessageOutputData outputData);
    }
}