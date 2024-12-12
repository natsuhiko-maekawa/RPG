using System;

namespace BattleScene.InterfaceAdapter.States.Turn
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
            var actor = Context.Actor ?? throw new InvalidOperationException();
            Context.TargetList = new[] { actor };
            Context.TransitionTo(_skillState);
        }
    }
}