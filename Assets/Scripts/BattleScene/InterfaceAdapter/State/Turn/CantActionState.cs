using System;
using System.Collections.Generic;
using BattleScene.Domain.Id;

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
            if (Context.ActorId == null) throw new InvalidOperationException();
            Context.TargetIdList = new List<CharacterId> { Context.ActorId };
            Context.TransitionTo(_skillState);
        }
    }
}