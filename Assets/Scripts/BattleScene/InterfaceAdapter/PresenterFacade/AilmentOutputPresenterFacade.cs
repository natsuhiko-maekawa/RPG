using System.Linq;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Presenter;
using static BattleScene.InterfaceAdapter.Presenter.PlayerImageViewPresenter.AnimationMode;

namespace BattleScene.InterfaceAdapter.PresenterFacade
{
    public class AilmentOutputPresenterFacade
    {
        private readonly IResource<AilmentViewDto, AilmentCode, SlipCode> _ailmentViewResource;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;

        public AilmentOutputPresenterFacade(
            IResource<AilmentViewDto, AilmentCode, SlipCode> ailmentViewResource,
            IRepository<CharacterEntity, CharacterId> characterRepository,
            MessageViewPresenter messageView,
            PlayerImageViewPresenter playerImageView)
        {
            _ailmentViewResource = ailmentViewResource;
            _characterRepository = characterRepository;
            _messageView = messageView;
            _playerImageView = playerImageView;
        }

        public void OutputThenAilmentFailure()
        {
            _messageView.StartAnimation(MessageCode.FailAilmentMessage);
        }

        public void OutputThenAilmentSuccess(BattleEventValueObject ailment)
        {
            _messageView.StartAnimation(MessageCode.AilmentMessage);

            if (ailment.ActualTargetIdList.Any(x => _characterRepository.Get(x).IsPlayer))
            {
                var playerImageCode = _ailmentViewResource.Get(ailment.AilmentCode).PlayerImageCode;
                _playerImageView.StartAnimation(playerImageCode, Slide);
            }
        }
    }
}