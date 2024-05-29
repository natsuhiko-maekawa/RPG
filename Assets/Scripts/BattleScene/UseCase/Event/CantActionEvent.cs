using System;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.EventRunner;
using BattleScene.UseCase.View.MessageView.OutputDataFactory;
using BattleScene.UseCase.View.PlayerImageView.OutputBoundary;
using BattleScene.UseCase.View.PlayerImageView.OutputDataFactory;
using static BattleScene.UseCase.EventRunner.EventCode;

namespace BattleScene.UseCase.Event
{
    internal class CantActionEvent : IEvent, IWait
    {
        private readonly AilmentDomainService _ailment;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly PlayerImageOutputDataFactory _playerImageOutputDataFactory;
        private readonly IAilmentViewInfoFactory _ailmentViewInfoFactory;
        private readonly ICharacterRepository _characterRepository;
        private readonly IPlayerImageViewPresenter _playerImageViewPresenter;
        
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