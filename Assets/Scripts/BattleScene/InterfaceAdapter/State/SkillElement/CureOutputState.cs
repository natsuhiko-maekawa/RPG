using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Facade;

namespace BattleScene.InterfaceAdapter.State.SkillElement
{
    public class CureOutputState : PrimeSkillOutputState<CureValueObject>
    {
        private readonly CureOutput _output;
        private readonly PrimeSkillStopState<CureValueObject> _primeSkillStopState;

        public CureOutputState(
            CureOutput output,
            PrimeSkillStopState<CureValueObject> primeSkillStopState)
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