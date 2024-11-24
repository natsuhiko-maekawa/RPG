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

        public void OutputThenSlipFailureAsync()
        {
            _messageView.StartAnimation(MessageCode.FailAilmentMessage);
        }

        public async Task OutputThenSlipSuccessAsync(BattleEventValueObject battleEvent)
        {
            var animationList = new List<Task>();
            _messageView.StartAnimation(MessageCode.AilmentMessage);

            var playerImageCode = _ailmentViewResource.Get(battleEvent.SlipCode).PlayerImageCode;
            var playerImageAnimation = _playerImageView.StartAnimationAsync(playerImageCode);
            animationList.Add(playerImageAnimation);

            await Task.WhenAll(animationList);
        }
    }
}