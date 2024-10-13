using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;
using static BattleScene.UseCases.OldEvent.Runner.EventCode;

namespace BattleScene.UseCases.OldEvent
{
    internal class AilmentsResetOldEvent : IOldEvent, IWait
    {
        private readonly ICollection<AilmentEntity, (CharacterId, AilmentCode)> _ailmentCollection;
        private readonly PlayerDomainService _player;
        private readonly OrderedItemsDomainService _orderedItems;

        public AilmentsResetOldEvent(
            ICollection<AilmentEntity, (CharacterId, AilmentCode)> ailmentCollection,
            OrderedItemsDomainService orderedItems)
        {
            _ailmentCollection = ailmentCollection;
            _orderedItems = orderedItems;
        }

        public EventCode Run()
        {
            var playerId = _player.GetId();
            if (_orderedItems.First().TryGetAilmentCode(out var ailmentCode) && ailmentCode == AilmentCode.Confusion) { }
                // _skillRepository.Update(_skillCreator.Create(playerId, SkillCode.Attack));

            _ailmentCollection.Remove((playerId, ailmentCode));
            StartView();

            return WaitEvent;
        }

        public EventCode NextEvent()
        {
            return LoopEndEvent;
        }

        private void StartView()
        {
            // 状態異常を表示
            // var ailmentOutputData = _ailmentOutputDataFactory.Create(_player.GetId());
            // _ailmentView.Start(ailmentOutputData);
            // メッセージを表示
            // var messageOutputData = _messageOutputDataFactory.Create(RecoverAilmentMessage);
            // _messageView.Start(messageOutputData);
            // プレイイヤーのデフォルトの立ち絵を表示
            // _playerImageView.Start(new PlayerImageOutputData(DefaultImage));
        }
    }
}