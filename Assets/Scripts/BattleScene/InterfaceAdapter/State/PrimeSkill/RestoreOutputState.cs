using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Facade;

namespace BattleScene.InterfaceAdapter.State.PrimeSkill
{
    public class RestoreOutputState : PrimeSkillOutputState<RestoreParameterValueObject, PrimeSkillValueObject>
    {
        private readonly RestoreOutputFacade _restoreOutput;
        private readonly PrimeSkillStopState<RestoreParameterValueObject, PrimeSkillValueObject> _primeSkillStopState;

        public RestoreOutputState(
            RestoreOutputFacade restoreOutput,
            PrimeSkillStopState<RestoreParameterValueObject, PrimeSkillValueObject> primeSkillStopState)
        {
            _restoreOutput = restoreOutput;
            _primeSkillStopState = primeSkillStopState;
        }

        public override async void Start()
        {
            await _restoreOutput.Output();
        }

        public override void Select()
        {
            Context.TransitionTo(_primeSkillStopState);
        }
    }
}