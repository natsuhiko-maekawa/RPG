using BattleScene.UseCases.UseCase;
using VContainer;

namespace BattleScene.UseCases.StateMachine
{
    internal class EnemySelectSkillState : AbstractState
    {
        private readonly EnemySelectSkill _enemySelectSkill;
        private readonly IObjectResolver _container;

        public EnemySelectSkillState(
            EnemySelectSkill enemySelectSkill,
            IObjectResolver container)
        {
            _enemySelectSkill = enemySelectSkill;
            _container = container;
        }

        public override void Start()
        {
            _enemySelectSkill.Execute();
            _container.Resolve<SkillStateFactory>().Create(null);
        }
    }
}