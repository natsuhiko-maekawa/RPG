using BattleScene.Domain.Code;
using BattleScene.UseCases.View.MessageView.OutputBoundary;

namespace BattleScene.UseCases.Output
{
    public class MessageView
    {
        private readonly IMessageViewPresenter _messageView;
        
        public void Out(MessageCode messageCode, bool noWait=false)
        {
            _messageView.Start(messageCode: messageCode, noWait: noWait);
        }
    }
}