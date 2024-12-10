using System.Linq;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.InterfaceAdapter.Presenter;
using static BattleScene.InterfaceAdapter.Presenter.PlayerImageViewPresenter.AnimationMode;

namespace BattleScene.InterfaceAdapter.PresenterFacade
{
    public class AilmentOutputPresenterFacade
    {
        private readonly IResource<AilmentViewDto, AilmentCode, SlipCode> _ailmentViewResource;
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;

        public AilmentOutputPresenterFacade(
            IResource<AilmentViewDto, AilmentCode, SlipCode> ailmentViewResource,
            MessageViewPresenter messageView,
            PlayerImageViewPresenter playerImageView)
        {
            _ailmentViewResource = ailmentViewResource;
            _messageView = messageView;
            _playerImageView = playerImageView;
        }

        public void OutputThenAilmentFailure()
        {
            _messageView.StartAnimation(MessageCode.FailAilmentMessage);
        }

        public void OutputThenAilmentSuccess(BattleEventEntity ailmentEvent)
        {
            _messageView.StartAnimation(MessageCode.AilmentMessage);

            if (ailmentEvent.ActualTargetList.Any(x => x.IsPlayer))
            {
                var playerImageCode = _ailmentViewResource.Get(ailmentEvent.AilmentCode).PlayerImageCode;
                _playerImageView.StartAnimation(playerImageCode, Slide);
            }
        }
    }
}