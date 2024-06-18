using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.Event.Runner;
using BattleScene.UseCase.View.AilmentView.OutputBoundary;
using BattleScene.UseCase.View.AilmentView.OutputDataFactory;
using BattleScene.UseCase.View.EnemyView.OutputBoundary;
using BattleScene.UseCase.View.EnemyView.OutputDataFactory;
using BattleScene.UseCase.View.MessageView.OutputBoundary;
using BattleScene.UseCase.View.MessageView.OutputDataFactory;
using static BattleScene.UseCase.Event.Runner.EventCode;
using static BattleScene.Domain.Code.MessageCode;

namespace BattleScene.UseCase.Event
{
    internal class EnemySuicideEvent : IEvent, IWait
    {
        private readonly IRepository<ActionTimeEntity, CharacterId> _actionTimeRepository;
        private readonly AilmentOutputDataFactory _ailmentOutputDataFactory;
        private readonly IAilmentRepository _ailmentRepository;
        private readonly IAilmentViewPresenter _ailmentView;
        private readonly EnemyOutputDataFactory _enemyOutputDataFactory;
        private readonly IEnemyViewPresenter _enemyView;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageView;
        private readonly OrderedItemsDomainService _orderedItems;

        public EnemySuicideEvent(
            IRepository<ActionTimeEntity, CharacterId> actionTimeRepository,
            AilmentOutputDataFactory ailmentOutputDataFactory,
            IAilmentRepository ailmentRepository,
            IAilmentViewPresenter ailmentView,
            EnemyOutputDataFactory enemyOutputDataFactory,
            IEnemyViewPresenter enemyView,
            MessageOutputDataFactory messageOutputDataFactory,
            IMessageViewPresenter messageView,
            OrderedItemsDomainService orderedItems)
        {
            _actionTimeRepository = actionTimeRepository;
            _ailmentOutputDataFactory = ailmentOutputDataFactory;
            _ailmentRepository = ailmentRepository;
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
            _ailmentRepository.Delete(characterId);
            _actionTimeRepository.Delete(characterId);

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