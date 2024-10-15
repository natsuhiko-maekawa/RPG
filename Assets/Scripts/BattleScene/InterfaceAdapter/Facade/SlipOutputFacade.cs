using System.Collections.Generic;
using System.Threading.Tasks;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.Facade
{
    public class SlipOutputFacade
    {
        private readonly IResource<AilmentViewDto, AilmentCode, SlipCode> _ailmentViewResource;
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;

        public SlipOutputFacade(
            IResource<AilmentViewDto, AilmentCode, SlipCode> ailmentViewResource,
            MessageViewPresenter messageView, 
            PlayerImageViewPresenter playerImageView)
        {
            _ailmentViewResource = ailmentViewResource;
            _messageView = messageView;
            _playerImageView = playerImageView;
        }

        public async Task OutputThenSlipFailureAsync()
        {
            var animationList = new List<Task>();
            var messageAnimation = _messageView.StartAnimationAsync(MessageCode.FailAilmentMessage);
            animationList.Add(messageAnimation);

            await Task.WhenAll(animationList);
        }

        public async Task OutputThenSlipSuccessAsync(PrimeSkillValueObject primeSkill)
        {
            var animationList = new List<Task>();
            var messageAnimation = _messageView.StartAnimationAsync(MessageCode.AilmentMessage);
            animationList.Add(messageAnimation);

            var playerImageCode = _ailmentViewResource.Get(primeSkill.SlipCode).PlayerImageCode;
            var playerImageAnimation = _playerImageView.StartAnimationAsync(playerImageCode);
            animationList.Add(playerImageAnimation);
            
            await Task.WhenAll(animationList);
        }
    }
}