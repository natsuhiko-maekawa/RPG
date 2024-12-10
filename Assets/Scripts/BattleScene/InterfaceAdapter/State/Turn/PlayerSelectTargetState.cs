using System;
using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.InterfaceAdapter.PresenterFacade;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    public class PlayerSelectTargetState : BaseState, ICancelable
    {
        private readonly SkillState _skillState;
        private readonly PlayerSelectTargetPresenterFacade _facade;

        public PlayerSelectTargetState(
            SkillState skillState,
            PlayerSelectTargetPresenterFacade facade)
        {
            _skillState = skillState;
            _facade = facade;
        }

        public override void Start()
        {
            if (Context.Skill == null) throw new InvalidOperationException(ExceptionMessage.ContextSkillIsNull);
            _facade.Output(Context.Actor!, Context.Skill);
        }

        public override void Select(IReadOnlyList<CharacterId> targetIdList)
        {
            _facade.Stop();
            Context.TargetIdList = targetIdList;
            Context.TransitionTo(_skillState);
        }

        public void OnCancel()
        {
            _facade.Stop();
        }
    }
}