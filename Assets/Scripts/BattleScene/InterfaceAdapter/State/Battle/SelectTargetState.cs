using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.InterfaceAdapter.Presenter;
using BattleScene.InterfaceAdapter.State.Skill;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class SelectTargetState : AbstractState
    {
        private readonly SkillState _skillState;
        private readonly TargetViewPresenter _targetView;

        public SelectTargetState(
            SkillState skillState,
            TargetViewPresenter targetView)
        {
            _skillState = skillState;
            _targetView = targetView;
        }

        public override void Start()
        {
            _targetView.StartAnimation(Context.SkillCode);
        }

        public override void Select(IReadOnlyList<CharacterId> targetIdList)
        {
            _targetView.StopAnimation();
            Context.TargetIdList = targetIdList;
            Context.TransitionTo(_skillState);
        }
    }
}