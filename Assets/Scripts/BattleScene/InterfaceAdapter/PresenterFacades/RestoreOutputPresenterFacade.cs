﻿using BattleScene.Domain.Codes;
using BattleScene.InterfaceAdapter.Presenters;

namespace BattleScene.InterfaceAdapter.PresenterFacades
{
    public class RestoreOutputPresenterFacade
    {
        private readonly MessageViewPresenter _messageView;
        private readonly RestoreViewPresenter _restoreView;

        public RestoreOutputPresenterFacade(
            MessageViewPresenter messageView,
            RestoreViewPresenter restoreView)
        {
            _messageView = messageView;
            _restoreView = restoreView;
        }

        public void Output()
        {
            _messageView.StartAnimation(MessageCode.RestoreMessage);
            _restoreView.StartAnimation();
        }
    }
}