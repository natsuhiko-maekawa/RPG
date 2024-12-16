using System;
using BattleScene.Domain.Codes;
using BattleScene.Domain.Entities;
using BattleScene.Presenters.Presenters;

namespace BattleScene.Presenters.PresenterFacades
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