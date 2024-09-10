using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;

namespace BattleScene.UseCases.OldEvent
{
    internal class SlipDamageMessageOldEvent : IOldEvent, IWait
    {
        private readonly IAilmentRepository _ailmentRepository;
        private readonly CharactersDomainService _characters;
        private readonly OrderedItemsDomainService _orderedItems;

        public SlipDamageMessageOldEvent(
            IAilmentRepository ailmentRepository,
            CharactersDomainService characters,
            OrderedItemsDomainService orderedItems)
        {
            _ailmentRepository = ailmentRepository;
            _characters = characters;
            _orderedItems = orderedItems;
        }

        public EventCode Run()
        {
            var playerId = _characters.GetPlayerId();
            var ailmentCode = _orderedItems.FirstAilmentCode();
            var ailment = _ailmentRepository.Select(playerId, ailmentCode);
            ailment.AdvanceTurn();

            // メッセージを表示してプレイヤーの立ち絵を更新
            // var slipDamageViewInfo = _slipDamageViewInfoFactory.Create(_orderedItems.FirstSlipDamageCode());
            // var messageOutputData = _ailmentMessageOutputDataFactory.Create();
            // _messageView.Start(messageOutputData);
            // var playerImageOutputData = _playerImageOutputDataFactory.Create(slipDamageViewInfo.PlayerImageCode);
            // _playerImageViewPresenter.Start(playerImageOutputData);

            return EventCode.WaitEvent;
        }

        public EventCode NextEvent()
        {
            return EventCode.AilmentsSlipDamageEvent;
        }
    }
}