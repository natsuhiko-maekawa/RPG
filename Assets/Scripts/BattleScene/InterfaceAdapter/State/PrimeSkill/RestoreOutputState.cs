using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Facade;

namespace BattleScene.InterfaceAdapter.State.PrimeSkill
{
    public class RestoreOutputState : PrimeSkillOutputState<RestoreValueObject>
    {
        private readonly RestoreOutputFacade _restoreOutput;
        private readonly PrimeSkillStopState<RestoreValueObject> _primeSkillStopState;

        public RestoreOutputState(
            RestoreOutputFacade restoreOutput,
            PrimeSkillStopState<RestoreValueObject> primeSkillStopState)
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