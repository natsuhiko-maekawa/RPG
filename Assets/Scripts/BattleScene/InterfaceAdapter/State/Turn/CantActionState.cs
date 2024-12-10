using System;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    public class CantActionState : BaseState
    {
        private readonly SkillState _skillState;

        public CantActionState(
            SkillState skillState)
        {
            _skillState = skillState;
        }

        public override void Start()
        {
            if (Context.ActorId is null) throw new InvalidOperationException();
            Context.TargetIdList = new[] { Context.ActorId };
            Context.TransitionTo(_skillState);
        }
    }
}