using BattleScene.Domain.DomainService;
using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;
using BattleScene.UseCases.View.AilmentView.OutputBoundary;
using BattleScene.UseCases.View.AilmentView.OutputDataFactory;
using BattleScene.UseCases.View.EnemyView.OutputBoundary;
using BattleScene.UseCases.View.EnemyView.OutputDataFactory;
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
        private readonly EnemyOutputDataFactory _enemyOutputDataFactory;
        private readonly IEnemyViewPresenter _enemyView;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageView;
        private readonly OrderedItemsDomainService _orderedItems;

        public EnemySuicideOldEvent(
            AilmentOutputDataFactory ailmentOutputDataFactory,
            IAilmentViewPresenter ailmentView,
            EnemyOutputDataFactory enemyOutputDataFactory,
            IEnemyViewPresenter enemyView,
            MessageOutputDataFactory messageOutputDataFactory,
            IMessageViewPresenter messageView,
            OrderedItemsDomainService orderedItems)
        {
            _ailmentOutputDataFactory = ailmentOutputDataFactory;
            _ailmentView = ailmentView;
            _enemyOutputDataFactory = enemyOutputDataFactory;
            _enemyView = enemyView;
            _messageOutputDataFactory = messageOutputDataFactory;
            _messageView = messageView;
            _orderedItems = orderedItems;
        }

        public EventCode Run()
        {
            var characterId = _orderedItems.FirstCharacterId();
            // 自滅した敵の状態異常を削除
            // _ailmentRepository.Delete(characterId);
            // 自滅した敵の行動時間を削除
            // _actionTimeRepository.Delete(characterId);

            var ailmentOutputData = _ailmentOutputDataFactory.Create(characterId);
            _ailmentView.Start(ailmentOutputData);
            var enemyOutputData = _enemyOutputDataFactory.Create();
            _enemyView.Start(enemyOutputData);
            var messageOutputData = _messageOutputDataFactory.Create(EnemySuicideMessage);
            _messageView.Start(messageOutputData);

            return WaitEvent;
        }

        public EventCode NextEvent()
        {
            return EventCode.LoopEndEvent;
        }
    }
}