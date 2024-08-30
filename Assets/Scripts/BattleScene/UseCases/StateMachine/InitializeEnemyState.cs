using BattleScene.UseCases.UseCase;
using BattleScene.UseCases.View.EnemyView;
using VContainer;

namespace BattleScene.UseCases.StateMachine
{
    internal class InitializeEnemyState : AbstractState
    {
        private readonly BattleStart _battleStart;
        private readonly EnemyViewOutput _enemyView;
        private readonly IObjectResolver _container;

        public InitializeEnemyState(
            BattleStart battleStart,
            EnemyViewOutput enemyView,
            IObjectResolver container)
        {
            _battleStart = battleStart;
            _enemyView = enemyView;
            _container = container;
        }

        public override void Start()
        {
            _battleStart.Execute();
            _enemyView.Out();
            Context.TransitionTo(_container.Resolve<OrderState>());
        }
    }
}