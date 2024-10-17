using System.Collections.Generic;
using System.Threading.Tasks;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.Facade
{
    public class RestoreOutputFacade
    {
        private readonly MessageViewPresenter _messageView;
        private readonly RestoreViewPresenter _restoreView;

        public RestoreOutputFacade(
            MessageViewPresenter messageView,
            RestoreViewPresenter restoreView)
        {
            _messageView = messageView;
            _restoreView = restoreView;
        }

        public async Task Output()
        {
            var animationList = new List<Task>();

            var messageAnimation = _messageView.StartAnimationAsync(MessageCode.RestoreMessage);
            animationList.Add(messageAnimation);

            var restoreAnimation = _restoreView.StartAnimationAsync();
            animationList.Add(restoreAnimation);

            await Task.WhenAll(animationList);
        }
    }
}