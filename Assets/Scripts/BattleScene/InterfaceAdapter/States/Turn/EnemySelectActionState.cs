using System;
using BattleScene.UseCases.UseCase;

namespace BattleScene.InterfaceAdapter.States.Turn
{
    public class EnemySelectActionState : BaseState
    {
        private readonly EnemySelectActionUseCase _enemySelectAction;
        private readonly SkillState _skillState;

        public EnemySelectActionState(
            EnemySelectActionUseCase enemySelectAction,
            SkillState skillState)
        {
            _enemySelectAction = enemySelectAction;
            _skillState = skillState;
        }

        public override void Start()
        {
            if (Context.Actor is null)
                throw new InvalidOperationException(ExceptionMessage.ContextActorIdIsNull);
            Context.Skill = _enemySelectAction.SelectSkill(Context.Actor);
            Context.TargetList = _enemySelectAction.SelectTarget(Context.Actor, Context.Skill);
            Context.TransitionTo(_skillState);
        }
    }
}