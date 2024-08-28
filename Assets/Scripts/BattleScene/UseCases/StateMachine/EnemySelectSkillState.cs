using BattleScene.UseCases.StateMachine.SkillStack;
using VContainer;

namespace BattleScene.UseCases.StateMachine
{
    internal class EnemySelectSkillState : AbstractState
    {
        private readonly IObjectResolver _container;

        public EnemySelectSkillState(
            IObjectResolver container)
        {
            _container = container;
        }

        public override void Start()
        {
            _container.Resolve<SkillContextStack>();
        }
    }
}