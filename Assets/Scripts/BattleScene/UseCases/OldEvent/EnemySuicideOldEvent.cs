using BattleScene.Domain.DomainService;
using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;
using BattleScene.UseCases.View.AilmentView.OutputBoundary;
using BattleScene.UseCases.View.AilmentView.OutputDataFactory;
using BattleScene.UseCases.View.MessageView.OutputBoundary;
using BattleScene.UseCases.View.MessageView.OutputDataFactory;
using static BattleScene.UseCases.OldEvent.Runner.EventCode;
using static BattleScene.Domain.Code.MessageCode;

namespace BattleScene.UseCases.OldEvent
{
    internal class EnemySuicideOldEvent : IOldEvent, IWait
    {
        private readonly AilmentOutputDataFactory _ailmentOutputDataFactory;
        private readonly IAilmentViewPresenter _ailmentView;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageView;
        private readonly OrderedItemsDomainService _orderedItems;

        public EnemySuicideOldEvent(
            AilmentOutputDataFactory ailmentOutputDataFactory,
            IAilmentViewPresenter ailmentView,
            MessageOutputDataFactory messageOutputDataFactory,
            IMessageViewPresenter messageView,
            OrderedItemsDomainService orderedItems)
        {
            _ailmentOutputDataFactory = ailmentOutputDataFactory;
            _ailmentView = ailmentView;
            _messageOutputDataFactory = messageOutputDataFactory;
            _messageView = messageView;
            _orderedItems = orderedItems;
        }

        public EventCode Run()
        {
            _orderedItems.First().TryGetCharacterId(out var characterId);
            // 自滅した敵の状態異常を削除
            // _ailmentRepository.Delete(characterId);
            // 自滅した敵の行動時間を削除
            // _actionTimeRepository.Delete(characterId);

            var ailmentOutputData = _ailmentOutputDataFactory.Create(characterId);
            _ailmentView.Start(ailmentOutputData);
            // 敵の画像の表示を更新
            // var enemyOutputData = _enemyOutputDataFactory.Create();
            // _enemyView.Start(enemyOutputData);
            var messageOutputData = _messageOutputDataFactory.Create(EnemySuicideMessage);
            _messageView.Start(messageOutputData);

            return WaitEvent;
        }

        public EventCode NextEvent()
        {
            return LoopEndEvent;
        }
    }
}