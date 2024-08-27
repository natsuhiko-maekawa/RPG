using BattleScene.UseCases.UseCase;

namespace BattleScene.UseCases.StateMachine
{
    internal class EnemySelectSkillState : AbstractState
    {
        private readonly EnemySelectSkill _enemySelectSkill;

        public EnemySelectSkillState(
            EnemySelectSkill enemySelectSkill)
        {
            _enemySelectSkill = enemySelectSkill;
        }

        public override void Start()
        {
            _enemySelectSkill.Execute();
        }
    }
}