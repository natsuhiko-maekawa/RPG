using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.EventRunner;
using BattleScene.UseCase.View.AilmentView.OutputBoundary;
using BattleScene.UseCase.View.AilmentView.OutputDataFactory;
using BattleScene.UseCase.View.EnemyView.OutputBoundary;
using BattleScene.UseCase.View.EnemyView.OutputDataFactory;
using BattleScene.UseCase.View.MessageView.OutputBoundary;
using BattleScene.UseCase.View.MessageView.OutputDataFactory;
using static BattleScene.UseCase.EventRunner.EventCode;
using static BattleScene.Domain.Code.MessageCode;

namespace BattleScene.UseCase.Event
{
    internal class EnemySuicideEvent : IEvent, IWait
    {
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly AilmentOutputDataFactory _ailmentOutputDataFactory;
        private readonly EnemyOutputDataFactory _enemyOutputDataFactory;
        private readonly IAilmentRepository _ailmentRepository;
        private readonly IActionTimeRepository _actionTimeRepository;
        private readonly IAilmentViewPresenter _ailmentView;
        private readonly IEnemyViewPresenter _enemyView;
        private readonly IMessageViewPresenter _messageView;

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