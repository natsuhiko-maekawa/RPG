using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Facade;

namespace BattleScene.InterfaceAdapter.State.SkillElement
{
    public class RestoreOutputState : SkillElementOutputState<RestoreValueObject>
    {
        private readonly RestoreOutputFacade _restoreOutput;
        private readonly SkillElementStopState<RestoreValueObject> _skillElementStopState;

        public RestoreOutputState(
            RestoreOutputFacade restoreOutput,
            SkillElementStopState<RestoreValueObject> skillElementStopState)
        {
            _restoreOutput = restoreOutput;
            _skillElementStopState = skillElementStopState;
        }

        public override async void Start()
        {
            await _restoreOutput.Output();
        }

        public override void Select()
        {
            Context.TransitionTo(_skillElementStopState);
        }
    }
}