using BattleScene.UseCases.UseCase;
using BattleScene.UseCases.View.EnemyView;
using VContainer;

namespace BattleScene.UseCases.StateMachine
{
    internal class InitializationEnemyState : AbstractState
    {
        private readonly BattleStart _battleStart;
        private readonly EnemyViewOutput _enemyView;
        private readonly IObjectResolver _container;

        public InitializationEnemyState(
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
        }

        public override void Select()
        {
            Context.TransitionTo(_container.Resolve<InitializationEnemyState>());
        }
    }
}