using BattleScene.Domain.Code;
using BattleScene.UseCases.IPresenter;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class PlayerSelectSkillState : AbstractState
    {
        private readonly SelectTargetStateFactory _selectTargetStateFactory;
        private readonly ISkillViewPresenter _skillView;

        public PlayerSelectSkillState(
            SelectTargetStateFactory selectTargetStateFactory,
            ISkillViewPresenter skillView)
        {
            _selectTargetStateFactory = selectTargetStateFactory;
            _skillView = skillView;
        }

        public override void Start()
        {
            _skillView.Start();
        }

        public override void Select(SkillCode skillCode)
        {
            var selectTargetState = _selectTargetStateFactory.Create(skillCode);
            Context.TransitionTo(selectTargetState);
        }
    }
}