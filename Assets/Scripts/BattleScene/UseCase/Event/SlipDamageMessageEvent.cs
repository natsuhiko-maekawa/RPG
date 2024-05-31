using BattleScene.Domain.DomainService;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.Event.Runner;
using BattleScene.UseCase.View.AilmentView.OutputDataFactory;
using BattleScene.UseCase.View.MessageView.OutputBoundary;
using BattleScene.UseCase.View.MessageView.OutputDataFactory;
using BattleScene.UseCase.View.PlayerImageView.OutputBoundary;
using BattleScene.UseCase.View.PlayerImageView.OutputDataFactory;

namespace BattleScene.UseCase.Event
{
    internal class SlipDamageMessageEvent : IEvent, IWait
    {
        private readonly AilmentMessageOutputDataFactory _ailmentMessageOutputDataFactory;
        private readonly SlipDamagePlayerImageOutputDataFactory _ailmentPlayerImageOutputDataFactory;
        private readonly IAilmentRepository _ailmentRepository;
        private readonly AilmentOutputDataFactory _ailmentViewInfoFactory;
        private readonly CharactersDomainService _characters;
        private readonly IMessageViewPresenter _messageView;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly PlayerImageOutputDataFactory _playerImageOutputDataFactory;
        private readonly IPlayerImageViewPresenter _playerImageViewPresenter;
        private readonly ISlipDamageViewInfoFactory _slipDamageViewInfoFactory;

        public EventCode Run()
        {
            var playerId = _characters.GetPlayerId();
            var ailmentCode = _orderedItems.FirstAilmentCode();
            var ailment = _ailmentRepository.Select(playerId, ailmentCode);
            ailment.AdvanceTurn();

            var slipDamageViewInfo = _slipDamageViewInfoFactory.Create(_orderedItems.FirstSlipDamageCode());
            var messageOutputData = _ailmentMessageOutputDataFactory.Create();
            _messageView.Start(messageOutputData);
            var playerImageOutputData = _playerImageOutputDataFactory.Create(slipDamageViewInfo.PlayerImageCode);
            _playerImageViewPresenter.Start(playerImageOutputData);

            return EventCode.WaitEvent;
        }

        public EventCode NextEvent()
        {
            return EventCode.AilmentsSlipDamageEvent;
        }
    }
}