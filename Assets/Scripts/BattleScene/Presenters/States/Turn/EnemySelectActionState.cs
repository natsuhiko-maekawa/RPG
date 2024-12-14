using System;
using BattleScene.Domain.Codes;
using BattleScene.UseCases.UseCases;

namespace BattleScene.Presenters.States.Turn
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
            var actor = Context.Actor ?? throw new InvalidOperationException(ExceptionMessage.ContextActorIdIsNull);
            Context.Skill = _enemySelectAction.SelectSkill(actor);
            Context.TargetList = _enemySelectAction.SelectTarget(actor, Context.Skill);
            Context.BattleEventCode = BattleEventCode.Skill;
            Context.TransitionTo(_skillState);
        }
    }
}