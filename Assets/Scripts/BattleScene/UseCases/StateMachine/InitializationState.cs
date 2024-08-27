using BattleScene.UseCases.UseCase;
using VContainer;

namespace BattleScene.UseCases.StateMachine
{
    internal class InitializationState : AbstractState
    {
        private readonly Initialization _initialization;
        private readonly IObjectResolver _container;

        public InitializationState(
            Initialization initialization, 
            IObjectResolver container)
        {
            _initialization = initialization;
            _container = container;
        }

        public override void Start()
        {
            _initialization.Execute();
            Context.TransitionTo(_container.Resolve<InitializationEnemyState>());
        }
    }
}