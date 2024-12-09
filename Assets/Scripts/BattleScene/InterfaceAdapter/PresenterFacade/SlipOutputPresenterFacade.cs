using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.InterfaceAdapter.Presenter;
using static BattleScene.InterfaceAdapter.Presenter.PlayerImageViewPresenter.AnimationMode;

namespace BattleScene.InterfaceAdapter.PresenterFacade
{
    public class SlipOutputPresenterFacade
    {
        private readonly IResource<AilmentViewDto, AilmentCode, SlipCode> _ailmentViewResource;
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;

        public SlipOutputPresenterFacade(
            IResource<AilmentViewDto, AilmentCode, SlipCode> ailmentViewResource,
            MessageViewPresenter messageView,
            PlayerImageViewPresenter playerImageView)
        {
            _ailmentViewResource = ailmentViewResource;
            _messageView = messageView;
            _playerImageView = playerImageView;
        }

        public void OutputThenSlipFailure()
        {
            _messageView.StartAnimation(MessageCode.FailAilmentMessage);
        }

        public void OutputThenSlipSuccess(BattleEventEntity slipEvent)
        {
            _messageView.StartAnimation(MessageCode.SlipMessage);

            var playerImageCode = _ailmentViewResource.Get(slipEvent.SlipCode).PlayerImageCode;
            _playerImageView.StartAnimation(playerImageCode, Slide);
        }
    }
}