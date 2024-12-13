using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.InterfaceAdapter.Presenters;
using static BattleScene.InterfaceAdapter.Presenters.PlayerImageViewPresenter.AnimationMode;

namespace BattleScene.InterfaceAdapter.PresenterFacades
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

        public void OutputWhenSlipFailure()
        {
            _messageView.StartAnimation(MessageCode.FailAilmentMessage);
        }

        public void OutputWhenSlipSuccess(BattleEventEntity slipEvent)
        {
            _messageView.StartAnimation(MessageCode.SlipMessage);

            var playerImageCode = _ailmentViewResource.Get(slipEvent.SlipCode).PlayerImageCode;
            _playerImageView.StartAnimation(playerImageCode, Slide);
        }
    }
}