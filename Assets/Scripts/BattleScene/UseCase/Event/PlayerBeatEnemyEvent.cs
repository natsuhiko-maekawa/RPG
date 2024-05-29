using System.Collections.Immutable;
using System.Linq;
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
    internal class PlayerBeatEnemyEvent : IEvent, IWait
    {
        private readonly MessageOutputDataFactory _messageGenerator;
        private readonly CharactersDomainService _characters;
        private readonly ResultDomainService _result;
        private readonly IAilmentRepository _ailmentRepository;
        private readonly IHitPointRepository _hitPointRepository;
        private readonly AilmentOutputDataFactory _ailmentOutputDataFactory;
        private readonly EnemyOutputDataFactory _enemyOutputDataFactory;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IAilmentViewPresenter _ailmentViewPresenter;
        private readonly IMessageViewPresenter _messageViewPresenter;
        private readonly IEnemyViewPresenter _enemyViewPresenter;
        
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