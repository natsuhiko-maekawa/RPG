using System.Collections.Generic;
using System.Threading.Tasks;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.Facade
{
    public class ResetOutputFacade
    {
        private readonly MessageViewPresenter _messageView;

        public ResetOutputFacade(
            MessageViewPresenter messageView)
        {
            _messageView = messageView;
        }

        public async Task Output()
        {
            var animationList = new List<Task>();

            var messageAnimation = _messageView.StartAnimationAsync(MessageCode.NoMessage);
            animationList.Add(messageAnimation);

            await Task.WhenAll(animationList);
        }
    }
}