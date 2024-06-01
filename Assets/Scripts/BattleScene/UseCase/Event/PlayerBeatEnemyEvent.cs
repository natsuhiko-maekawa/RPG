using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.DomainService;
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
    internal class PlayerBeatEnemyEvent : IEvent, IWait
    {
        private readonly AilmentOutputDataFactory _ailmentOutputDataFactory;
        private readonly IAilmentRepository _ailmentRepository;
        private readonly IAilmentViewPresenter _ailmentViewPresenter;
        private readonly CharactersDomainService _characters;
        private readonly EnemyOutputDataFactory _enemyOutputDataFactory;
        private readonly IEnemyViewPresenter _enemyViewPresenter;
        private readonly IHitPointRepository _hitPointRepository;
        private readonly MessageOutputDataFactory _messageGenerator;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageViewPresenter;
        private readonly ResultDomainService _result;

        public PlayerBeatEnemyEvent(
            AilmentOutputDataFactory ailmentOutputDataFactory,
            IAilmentRepository ailmentRepository,
            IAilmentViewPresenter ailmentViewPresenter,
            CharactersDomainService characters,
            EnemyOutputDataFactory enemyOutputDataFactory,
            IEnemyViewPresenter enemyViewPresenter,
            IHitPointRepository hitPointRepository,
            MessageOutputDataFactory messageGenerator,
            MessageOutputDataFactory messageOutputDataFactory,
            IMessageViewPresenter messageViewPresenter,
            ResultDomainService result)
        {
            _ailmentOutputDataFactory = ailmentOutputDataFactory;
            _ailmentRepository = ailmentRepository;
            _ailmentViewPresenter = ailmentViewPresenter;
            _characters = characters;
            _enemyOutputDataFactory = enemyOutputDataFactory;
            _enemyViewPresenter = enemyViewPresenter;
            _hitPointRepository = hitPointRepository;
            _messageGenerator = messageGenerator;
            _messageOutputDataFactory = messageOutputDataFactory;
            _messageViewPresenter = messageViewPresenter;
            _result = result;
        }

        public EventCode Run()
        {
            var deadEnemyList = _hitPointRepository.Select()
                .Where(x => !x.IsSurvive())
                .Select(x => x.CharacterId)
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
                .Select(x => x.CharacterId)
                .ToImmutableHashSet();
            var targetList = _result.LastDamage().DamageList
                .Select(x => x.TargetId)
                .Distinct()
                .ToImmutableHashSet();
            if (deadEnemyList == targetList)
                return EventCode.LoopEndEvent;

            return EventCode.SwitchSkillEvent;
        }
    }
}