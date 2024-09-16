using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class SelectTargetState : AbstractState
    {
        private readonly SkillStateFactory _skillStateFactory;
        private readonly TargetViewPresenter _targetView;
        private readonly SkillCode _skillCode;

        public SelectTargetState(
            SkillStateFactory skillStateFactory,
            TargetViewPresenter targetView,
            SkillCode skillCode)
        {
            _skillStateFactory = skillStateFactory;
            _targetView = targetView;
            _skillCode = skillCode;
        }

        public override void Start()
        {
            _targetView.StartAnimation(_skillCode);
        }

        public override void Select(IList<CharacterId> targetIdList)
        {
            _targetView.StopAnimation();
            Context.TransitionTo(_skillStateFactory.Create(
                skillCode: _skillCode,
                targetIdList: targetIdList));
        }
    }
}