using BattleScene.UseCases.StateMachine;
using BattleScene.UseCases.UseCase;

namespace BattleScene.UseCases.Event
{
    internal class EnemySkillSelectorEvent : BaseEvent
    {
        private readonly EnemySelectSkill _enemySelectSkill;

        public EnemySkillSelectorEvent(
            EnemySelectSkill enemySelectSkill)
        {
            _enemySelectSkill = enemySelectSkill;
        }

        public override void UseCase()
        {
            _enemySelectSkill.Execute();
        }

        public override void Output()
        {
            // throw new System.NotImplementedException();
        }

        public override StateCode GetStateCode()
        {
            return StateCode.EnemyAttack;
        }
    }
}