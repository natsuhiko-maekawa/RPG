using BattleScene.Domain.DomainService;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;
using BattleScene.UseCases.View.MessageView.OutputBoundary;
using BattleScene.UseCases.View.MessageView.OutputDataFactory;
using BattleScene.UseCases.View.PlayerImageView.OutputBoundary;
using BattleScene.UseCases.View.PlayerImageView.OutputDataFactory;

namespace BattleScene.UseCases.OldEvent
{
    internal class SlipDamageMessageOldEvent : IOldEvent, IWait
    {
        private readonly AilmentMessageOutputDataFactory _ailmentMessageOutputDataFactory;
        private readonly IAilmentRepository _ailmentRepository;
        private readonly CharactersDomainService _characters;
        private readonly IMessageViewPresenter _messageView;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly PlayerImageOutputDataFactory _playerImageOutputDataFactory;
        private readonly IPlayerImageViewPresenter _playerImageViewPresenter;
        private readonly ISlipDamageViewInfoFactory _slipDamageViewInfoFactory;

        public SlipDamageMessageOldEvent(
            AilmentMessageOutputDataFactory ailmentMessageOutputDataFactory,
            IAilmentRepository ailmentRepository,
            CharactersDomainService characters,
            IMessageViewPresenter messageView,
            OrderedItemsDomainService orderedItems,
            PlayerImageOutputDataFactory playerImageOutputDataFactory,
            IPlayerImageViewPresenter playerImageViewPresenter,
            ISlipDamageViewInfoFactory slipDamageViewInfoFactory)
        {
            _ailmentMessageOutputDataFactory = ailmentMessageOutputDataFactory;
            _ailmentRepository = ailmentRepository;
            _characters = characters;
            _messageView = messageView;
            _orderedItems = orderedItems;
            _playerImageOutputDataFactory = playerImageOutputDataFactory;
            _playerImageViewPresenter = playerImageViewPresenter;
            _slipDamageViewInfoFactory = slipDamageViewInfoFactory;
        }

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