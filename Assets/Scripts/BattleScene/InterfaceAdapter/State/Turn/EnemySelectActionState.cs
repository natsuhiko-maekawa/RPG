using System;
using BattleScene.UseCases.UseCase;

namespace BattleScene.InterfaceAdapter.State.Turn
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
            if (Context.ActorId == null)
                throw new InvalidOperationException(ExceptionMessage.ContextActorIdIsNull);
            Context.Skill = _enemySelectAction.SelectSkill(Context.ActorId);
            Context.TargetIdList = _enemySelectAction.SelectTarget(Context.ActorId, Context.Skill);
            Context.TransitionTo(_skillState);
        }
    }
}