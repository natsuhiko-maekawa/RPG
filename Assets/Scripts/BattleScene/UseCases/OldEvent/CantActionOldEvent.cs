using System;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;
using BattleScene.UseCases.View.MessageView.OutputDataFactory;
using BattleScene.UseCases.View.PlayerImageView.OutputBoundary;
using BattleScene.UseCases.View.PlayerImageView.OutputDataFactory;
using static BattleScene.UseCases.OldEvent.Runner.EventCode;

namespace BattleScene.UseCases.OldEvent
{
    internal class CantActionOldEvent : IOldEvent, IWait
    {
        private readonly AilmentDomainService _ailment;
        private readonly IAilmentViewInfoFactory _ailmentViewInfoFactory;
        private readonly ICharacterRepository _characterRepository;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly PlayerImageOutputDataFactory _playerImageOutputDataFactory;
        private readonly IPlayerImageViewPresenter _playerImageViewPresenter;

        public CantActionOldEvent(
            AilmentDomainService ailment,
            IAilmentViewInfoFactory ailmentViewInfoFactory,
            ICharacterRepository characterRepository,
            MessageOutputDataFactory messageOutputDataFactory,
            OrderedItemsDomainService orderedItems,
            PlayerImageOutputDataFactory playerImageOutputDataFactory,
            IPlayerImageViewPresenter playerImageViewPresenter)
        {
            _ailment = ailment;
            _ailmentViewInfoFactory = ailmentViewInfoFactory;
            _characterRepository = characterRepository;
            _messageOutputDataFactory = messageOutputDataFactory;
            _orderedItems = orderedItems;
            _playerImageOutputDataFactory = playerImageOutputDataFactory;
            _playerImageViewPresenter = playerImageViewPresenter;
        }

        public EventCode Run()
        {
            var ailment = _ailment.GetHighPriority(_orderedItems.FirstCharacterId());
            if (ailment == null) throw new InvalidOperationException();

            var ailmentViewInfo = _ailmentViewInfoFactory.Create(ailment.AilmentCode);
            _messageOutputDataFactory.Create(ailmentViewInfo.MessageCode);
            if (_characterRepository.Select(_orderedItems.FirstCharacterId()).IsPlayer())
            {
                var playerImageOutputData = _playerImageOutputDataFactory.Create(ailmentViewInfo.PlayerImageCode);
                _playerImageViewPresenter.Start(playerImageOutputData);
            }


            return WaitEvent;
        }

        public EventCode NextEvent()
        {
            return EventCode.LoopEndEvent;
        }
    }
}