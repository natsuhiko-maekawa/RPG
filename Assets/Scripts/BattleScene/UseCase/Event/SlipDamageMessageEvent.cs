﻿using BattleScene.Domain.DomainService;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.EventRunner;
using BattleScene.UseCase.View.AilmentView.OutputDataFactory;
using BattleScene.UseCase.View.MessageView.OutputBoundary;
using BattleScene.UseCase.View.MessageView.OutputDataFactory;
using BattleScene.UseCase.View.PlayerImageView.OutputBoundary;
using BattleScene.UseCase.View.PlayerImageView.OutputDataFactory;

namespace BattleScene.UseCase.Event
{
    internal class SlipDamageMessageEvent : IEvent, IWait
    {
        private readonly IAilmentRepository _ailmentRepository;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly AilmentOutputDataFactory _ailmentViewInfoFactory;
        private readonly ISlipDamageViewInfoFactory _slipDamageViewInfoFactory;
        private readonly CharactersDomainService _characters;
        private readonly AilmentMessageOutputDataFactory _ailmentMessageOutputDataFactory;
        private readonly SlipDamagePlayerImageOutputDataFactory _ailmentPlayerImageOutputDataFactory;
        private readonly PlayerImageOutputDataFactory _playerImageOutputDataFactory;
        private readonly IMessageViewPresenter _messageView;
        private readonly IPlayerImageViewPresenter _playerImageViewPresenter;
        
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