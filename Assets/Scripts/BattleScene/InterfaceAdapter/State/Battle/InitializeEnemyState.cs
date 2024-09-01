using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.UseCases.View.EnemyView.OutputBoundary;
using BattleScene.UseCases.View.EnemyView.OutputData;
using VContainer;
using static BattleScene.Domain.Code.CharacterTypeCode;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    internal class InitializeEnemyState : AbstractState
    {
        private readonly EnemiesDomainService _enemies;
        private readonly IEnemyViewPresenter _enemyView;
        private readonly IObjectResolver _container;

        public InitializeEnemyState(
            EnemiesDomainService enemies,
            IEnemyViewPresenter enemyView,
            IObjectResolver container)
        {
            _enemies = enemies;
            _enemyView = enemyView;
            _container = container;
        }

        public override void Start()
        {
            SetEnemies();
            StartEnemyView();
            Context.TransitionTo(_container.Resolve<OrderState>());
        }

        private void SetEnemies()
        {
            var enemyTypeIdList = new List<CharacterTypeCode> { Bee, Dragon, Mantis, Shuten, Slime };
            _enemies.Add(enemyTypeIdList);
        }
        
        private void StartEnemyView()
        {
            var enemyIdList = _enemies.Get()
                .Select(x => x.Id)
                .ToImmutableList();
            var enemyOutputData = new EnemyOutputData(enemyIdList);
            _enemyView.Start(enemyOutputData);
        }
    }
}