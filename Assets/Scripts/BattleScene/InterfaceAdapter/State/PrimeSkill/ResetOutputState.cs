using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Facade;

namespace BattleScene.InterfaceAdapter.State.PrimeSkill
{
    public class ResetOutputState : PrimeSkillOutputState<ResetParameterValueObject>
    {
        private readonly ResetOutputFacade _resetOutput;
        private readonly PrimeSkillStopState<ResetParameterValueObject> _primeSkillStopState;

        public ResetOutputState(
            ResetOutputFacade resetOutput,
            PrimeSkillStopState<ResetParameterValueObject> primeSkillStopState)
        {
            _resetOutput = resetOutput;
            _primeSkillStopState = primeSkillStopState;
        }

        public override async void Start()
        {
            await _resetOutput.Output();
        }

        public override void Select()
        {
            Context.TransitionTo(_primeSkillStopState);
        }
    }
}