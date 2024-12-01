using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.PresenterFacade;

namespace BattleScene.InterfaceAdapter.State.SkillElement
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