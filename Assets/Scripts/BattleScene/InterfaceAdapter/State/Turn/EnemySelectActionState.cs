using System;
using BattleScene.UseCases.IUseCase;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    public class EnemySelectActionState : BaseState
    {
        private readonly IEnemySelectActionUseCase _enemySelectAction;
        private readonly SkillState _skillState;

        public EnemySelectActionState(
            IEnemySelectActionUseCase enemySelectAction,
            SkillState skillState)
        {
            _enemySelectAction = enemySelectAction;
            _skillState = skillState;
        }

        public override void Start()
        {
            if (Context.ActorId == null)
                throw new InvalidOperationException("Context.ActorId expect has id but was null.");
            Context.Skill = _enemySelectAction.SelectSkill(Context.ActorId);
            // TODO: SkillCodeをSkillに置き換えた後、以下の一行を削除する
            Context.SkillCode = Context.Skill.SkillCommon.SkillCode;
            Context.TargetIdList = _enemySelectAction.SelectTarget(Context.ActorId, Context.Skill);
            Context.TransitionTo(_skillState);
        }
    }
}