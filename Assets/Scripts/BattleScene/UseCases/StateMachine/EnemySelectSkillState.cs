using BattleScene.UseCases.UseCase;
using VContainer;

namespace BattleScene.UseCases.StateMachine
{
    internal class EnemySelectSkillState : AbstractState
    {
        private readonly EnemySkillSelector _enemySkillSelector;
        private readonly IObjectResolver _container;

        public EnemySelectSkillState(
            EnemySkillSelector enemySkillSelector,
            IObjectResolver container)
        {
            _enemySkillSelector = enemySkillSelector;
            _container = container;
        }

        public override void Start()
        {
            var skillCode = _enemySkillSelector.Select();
            var nextState = _container.Resolve<SkillStateFactory>().Create(skillCode);
            Context.TransitionTo(nextState);
        }
    }
}