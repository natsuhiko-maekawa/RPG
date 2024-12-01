using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.Facade
{
    public class BuffOutputPresenterFacade
    {
        private readonly MessageViewPresenter _messageView;

        public BuffOutputPresenterFacade(
            MessageViewPresenter messageView)
        {
            _messageView = messageView;
        }

        public void Output(BattleEventValueObject buff)
        {
            var isBuff = Math.Sign(Math.Log(buff.Rate)) > 0;
            var messageCode = isBuff
                ? MessageCode.BuffMessage
                : MessageCode.DebuffMessage;
            _messageView.StartAnimation(messageCode);
        }
    }
}