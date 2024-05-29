using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.EventRunner;
using BattleScene.UseCase.Service;
using BattleScene.UseCase.View.AilmentView.OutputDataFactory;
using BattleScene.UseCase.View.AttackCountView.OutputBoundary;
using BattleScene.UseCase.View.AttackCountView.OutputDataFactory;
using BattleScene.UseCase.View.EnemyView.OutputBoundary;
using BattleScene.UseCase.View.EnemyView.OutputDataFactory;
using BattleScene.UseCase.View.HitPointBarView.OutputBoundary;
using BattleScene.UseCase.View.HitPointBarView.OutputDataFactory;
using BattleScene.UseCase.View.TechnicalPointBarView.OutputBoundary;
using BattleScene.UseCase.View.TechnicalPointBarView.OutputDaraFactory;
using static BattleScene.Domain.Code.CharacterTypeId;

namespace BattleScene.UseCase.Event
{
    internal class BattleStartEvent : IEvent
    {
        private readonly AilmentOutputDataFactory _ailmentView;
        private readonly AttackCountOutputDataFactory _attackCountOutputDataFactory;
        private readonly CharacterCreatorService _characterCreator;
        private readonly EnemyOutputDataFactory _enemyOutputDataFactory;
        private readonly HitPointBarOutputDataFactory _hitPointBarOutputDataFactory;
        private readonly TechnicalPointBarOutputDataFactory _technicalPointBarOutputDataFactory;
        private readonly ICharacterRepository _characterRepository;
        private readonly IAttackCountViewPresenter _attackCountView;
        private readonly IEnemyViewPresenter _enemyView;
        private readonly IHitPointBarViewPresenter _hitPointBarView;
        private readonly ITechnicalPointBarViewPresenter _technicalPointBarView;
        
        public EventCode Run()
        {
            var enemyTypeIdList = new List<CharacterTypeId> { Bee, Dragon, Mantis, Shuten, Slime };
            var enemyList = _characterCreator.CreateEnemyList(enemyTypeIdList);
            _characterRepository.Update(enemyList);
            
            _ailmentView.Create();
            _attackCountView.Start(_attackCountOutputDataFactory.Create());
            _enemyView.Start(_enemyOutputDataFactory.Create()); 
            _hitPointBarView.Start(_hitPointBarOutputDataFactory.Create());
            _technicalPointBarView.Start(_technicalPointBarOutputDataFactory.Create());
            
            return EventCode.OrderDecisionEvent;
        }
    }
}