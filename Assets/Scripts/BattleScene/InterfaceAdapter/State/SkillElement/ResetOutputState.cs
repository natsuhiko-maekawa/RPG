using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Facade;

namespace BattleScene.InterfaceAdapter.State.SkillElement
{
    public class ResetOutputState : SkillElementOutputState<RecoveryValueObject>
    {
        private readonly ResetOutputFacade _resetOutput;
        private readonly SkillElementStopState<RecoveryValueObject> _skillElementStopState;

        public ResetOutputState(
            ResetOutputFacade resetOutput,
            SkillElementStopState<RecoveryValueObject> skillElementStopState)
        {
            _resetOutput = resetOutput;
            _skillElementStopState = skillElementStopState;
        }

        public override void Start()
        {
            _resetOutput.Output();
        }

        public override void Select()
        {
            Context.TransitionTo(_skillElementStopState);
        }
    }
}