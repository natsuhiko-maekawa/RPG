using System;
using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    public class PlayerSelectTargetState : BaseState, ICancelable
    {
        private readonly SkillState _skillState;
        private readonly TargetViewPresenter _targetView;

        public PlayerSelectTargetState(
            SkillState skillState,
            TargetViewPresenter targetView)
        {
            _skillState = skillState;
            _targetView = targetView;
        }

        public override void Start()
        {
            if (Context.Skill == null) throw new InvalidOperationException(ExceptionMessage.ContextSkillIsNull);
            _targetView.StartAnimation(Context.Skill);
        }

        public override void Select(IReadOnlyList<CharacterId> targetIdList)
        {
            _targetView.StopAnimation();
            Context.TargetIdList = targetIdList;
            Context.TransitionTo(_skillState);
        }

        public void OnCancel()
        {
            _targetView.StopAnimation();
        }
    }
}