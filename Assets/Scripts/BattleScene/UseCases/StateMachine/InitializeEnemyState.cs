using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.UseCases.View.EnemyView;
using VContainer;
using static BattleScene.Domain.Code.CharacterTypeCode;

namespace BattleScene.UseCases.StateMachine
{
    internal class InitializeEnemyState : AbstractState
    {
        private readonly EnemiesDomainService _enemies;
        private readonly EnemyViewOutput _enemyView;
        private readonly IObjectResolver _container;

        public InitializeEnemyState(
            EnemiesDomainService enemies,
            EnemyViewOutput enemyView,
            IObjectResolver container)
        {
            _enemies = enemies;
            _enemyView = enemyView;
            _container = container;
        }

        public override void Start()
        {
            SetEnemies();
            _enemyView.Out();
            Context.TransitionTo(_container.Resolve<OrderState>());
        }

        private void SetEnemies()
        {
            var enemyTypeIdList = new List<CharacterTypeCode> { Bee, Dragon, Mantis, Shuten, Slime };
            _enemies.Add(enemyTypeIdList);
        }
    }
}