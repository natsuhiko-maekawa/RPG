using BattleScene.Domain.ValueObjects;
using BattleScene.InterfaceAdapter.PresenterFacades;

namespace BattleScene.InterfaceAdapter.States.Skill
{
    public class ResetOutputState : SkillElementOutputState<RecoveryValueObject>
    {
        private readonly ResetOutputPresenterFacade _facade;
        private readonly SkillElementStopState<RecoveryValueObject> _skillElementStopState;

        public ResetOutputState(
            ResetOutputPresenterFacade facade,
            SkillElementStopState<RecoveryValueObject> skillElementStopState)
        {
            _facade = facade;
            _skillElementStopState = skillElementStopState;
        }

        public override void Start()
        {
            _facade.Output();
        }

        public override void Select()
        {
            Context.TransitionTo(_skillElementStopState);
        }
    }
}