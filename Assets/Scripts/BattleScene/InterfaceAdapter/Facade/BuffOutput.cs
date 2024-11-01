using System;
using System.Threading.Tasks;
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

        public async Task Out(BattleEventValueObject buff)
        {
            var isBuff = Math.Sign(Math.Log(buff.Rate)) > 0;
            var messageCode = isBuff
                ? MessageCode.BuffMessage
                : MessageCode.DebuffMessage;
            await _messageView.StartAnimationAsync(messageCode);
        }
    }
}