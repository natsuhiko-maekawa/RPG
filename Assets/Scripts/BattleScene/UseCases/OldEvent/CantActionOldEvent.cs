using System;
using BattleScene.Domain.DomainService;
using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;
using static BattleScene.UseCases.OldEvent.Runner.EventCode;

namespace BattleScene.UseCases.OldEvent
{
    internal class CantActionOldEvent : IOldEvent, IWait
    {
        private readonly AilmentDomainService _ailment;
        private readonly OrderedItemsDomainService _orderedItems;

        public CantActionOldEvent(
            AilmentDomainService ailment,
            OrderedItemsDomainService orderedItems)
        {
            _ailment = ailment;
            _orderedItems = orderedItems;
        }

        public EventCode Run()
        {
            _orderedItems.First().TryGetCharacterId(out var characterId);
            var ailment = _ailment.GetHighestPriority(characterId);
            if (ailment == null) throw new InvalidOperationException();

            // 状態異常のメッセージを表示
            // プレイヤーの場合は立ち絵を更新
            // var ailmentViewInfo = _ailmentViewInfoFactory.Create(ailment.AilmentCode);
            // _messageOutputDataFactory.Create(ailmentViewInfo.MessageCode);
            // if (_characterRepository.Select(_orderedItems.FirstCharacterId()).IsPlayer())
            // {
            //     var playerImageOutputData = _playerImageOutputDataFactory.Create(ailmentViewInfo.PlayerImageCode);
            //     _playerImageViewPresenter.Start(playerImageOutputData);
            // }


            return WaitEvent;
        }

        public EventCode NextEvent()
        {
            return LoopEndEvent;
        }
    }
}