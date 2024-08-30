using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
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
    internal class PlayerBeatEnemyOldEvent : IOldEvent, IWait
    {
        private readonly AilmentOutputDataFactory _ailmentOutputDataFactory;
        private readonly IAilmentRepository _ailmentRepository;
        private readonly IAilmentViewPresenter _ailmentViewPresenter;
        private readonly EnemyOutputDataFactory _enemyOutputDataFactory;
        private readonly IEnemyViewPresenter _enemyViewPresenter;
        private readonly IRepository<HitPointAggregate, CharacterId> _hitPointRepository;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageViewPresenter;
        private readonly ResultDomainService _result;

        public PlayerBeatEnemyOldEvent(
            AilmentOutputDataFactory ailmentOutputDataFactory,
            IAilmentRepository ailmentRepository,
            IAilmentViewPresenter ailmentViewPresenter,
            EnemyOutputDataFactory enemyOutputDataFactory,
            IEnemyViewPresenter enemyViewPresenter,
            IRepository<HitPointAggregate, CharacterId> hitPointRepository,
            MessageOutputDataFactory messageOutputDataFactory,
            IMessageViewPresenter messageViewPresenter,
            ResultDomainService result)
        {
            _ailmentOutputDataFactory = ailmentOutputDataFactory;
            _ailmentRepository = ailmentRepository;
            _ailmentViewPresenter = ailmentViewPresenter;
            _enemyOutputDataFactory = enemyOutputDataFactory;
            _enemyViewPresenter = enemyViewPresenter;
            _hitPointRepository = hitPointRepository;
            _messageOutputDataFactory = messageOutputDataFactory;
            _messageViewPresenter = messageViewPresenter;
            _result = result;
        }

        public EventCode Run()
        {
            var deadEnemyList = _hitPointRepository.Select()
                .Where(x => !x.IsSurvive())
                .Select(x => x.Id)
                .ToImmutableList();

            _ailmentRepository.Delete(deadEnemyList);

            var ailmentOutputData = _ailmentOutputDataFactory.Create(deadEnemyList);
            _ailmentViewPresenter.Start(ailmentOutputData);
            var enemyOutputData = _enemyOutputDataFactory.Create();
            _enemyViewPresenter.Start(enemyOutputData);
            var messageOutputData = _messageOutputDataFactory.Create(BeatEnemyMessage);
            _messageViewPresenter.Start(messageOutputData);

            return WaitEvent;
        }

        public EventCode NextEvent()
        {
            var deadEnemyList = _hitPointRepository.Select()
                .Where(x => !x.IsSurvive())
                .Select(x => x.Id)
                .ToImmutableHashSet();
            var targetList = _result.LastDamage().AttackList
                .Select(x => x.TargetId)
                .Distinct()
                .ToImmutableHashSet();
            if (deadEnemyList == targetList)
                return EventCode.LoopEndEvent;

            return EventCode.SwitchSkillEvent;
        }
    }
}