using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.InterfaceAdapter.Presenters;

namespace BattleScene.InterfaceAdapter.PresenterFacades
{
    public class BuffOutputPresenterFacade
    {
        private readonly MessageViewPresenter _messageView;

        public BuffOutputPresenterFacade(
            MessageViewPresenter messageView)
        {
            _messageView = messageView;
        }

        public void Output(BattleEventEntity buffEvent)
        {
            var isBuff = Math.Sign(Math.Log(buffEvent.Rate)) > 0;
            var messageCode = isBuff
                ? MessageCode.BuffMessage
                : MessageCode.DebuffMessage;
            _messageView.StartAnimation(messageCode);
        }
    }
}