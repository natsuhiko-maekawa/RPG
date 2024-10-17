using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.Facade
{
    public class AilmentOutputFacade
    {
        private readonly IResource<AilmentViewDto, AilmentCode, SlipCode> _ailmentViewResource;
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;

        public AilmentOutputFacade(
            IResource<AilmentViewDto, AilmentCode, SlipCode> ailmentViewResource,
            ICollection<CharacterEntity, CharacterId> characterCollection,
            MessageViewPresenter messageView,
            PlayerImageViewPresenter playerImageView)
        {
            _ailmentViewResource = ailmentViewResource;
            _characterCollection = characterCollection;
            _messageView = messageView;
            _playerImageView = playerImageView;
        }

        public async Task OutputThenAilmentFailureAsync()
        {
            var animationList = new List<Task>();
            var messageAnimation = _messageView.StartAnimationAsync(MessageCode.FailAilmentMessage);
            animationList.Add(messageAnimation);

            await Task.WhenAll(animationList);
        }

        public async Task OutputThenAilmentSuccessAsync(PrimeSkillValueObject ailment)
        {
            var animationList = new List<Task>();
            var messageAnimation = _messageView.StartAnimationAsync(MessageCode.AilmentMessage);
            animationList.Add(messageAnimation);

            if (ailment.ActualTargetIdList.Any(x => _characterCollection.Get(x).IsPlayer))
            {
                var playerImageCode = _ailmentViewResource.Get(ailment.AilmentCode).PlayerImageCode;
                var playerImageAnimation = _playerImageView.StartAnimationAsync(playerImageCode);
                animationList.Add(playerImageAnimation);
            }

            await Task.WhenAll(animationList);
        }
    }
}