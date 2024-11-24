using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.Facade
{
    public class BuffOutput
    {
        private readonly MessageViewPresenter _messageView;

        public BuffOutput(
            MessageViewPresenter messageView)
        {
            _messageView = messageView;
        }

        public void Out(BattleEventValueObject buff)
        {
            var isBuff = Math.Sign(Math.Log(buff.Rate)) > 0;
            var messageCode = isBuff
                ? MessageCode.BuffMessage
                : MessageCode.DebuffMessage;
            _messageView.StartAnimation(messageCode);
        }
    }
}