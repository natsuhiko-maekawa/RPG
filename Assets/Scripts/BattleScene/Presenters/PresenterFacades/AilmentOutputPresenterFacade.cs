using System.Linq;
using BattleScene.DataAccesses;
using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using BattleScene.Domain.Entities;
using BattleScene.Presenters.Presenters;
using static BattleScene.Presenters.Presenters.PlayerImageViewPresenter.AnimationMode;

namespace BattleScene.Presenters.PresenterFacades
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

        public void OutputWhenAilmentFailure()
        {
            _messageView.StartAnimation(MessageCode.FailAilmentMessage);
        }

        public void OutputWhenAilmentSuccess(BattleEventEntity ailmentEvent)
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