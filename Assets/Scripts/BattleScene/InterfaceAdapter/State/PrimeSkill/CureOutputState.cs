using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Facade;

namespace BattleScene.InterfaceAdapter.State.PrimeSkill
{
    public class CureOutputState : PrimeSkillOutputState<CureParameterValueObject>
    {
        private readonly CureOutput _output;
        private readonly PrimeSkillStopState<CureParameterValueObject> _primeSkillStopState;

        public CureOutputState(
            CureOutput output,
            PrimeSkillStopState<CureParameterValueObject> primeSkillStopState)
        {
            _output = output;
            _primeSkillStopState = primeSkillStopState;
        }

        public override async void Start()
        {
            await _output.Output(Context.BattleEventQueue.Peek());
        }

        public override void Select()
        {
            Context.TransitionTo(_primeSkillStopState);
        }
    }
}