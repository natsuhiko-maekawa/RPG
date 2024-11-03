using System.Linq;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Facade;

namespace BattleScene.InterfaceAdapter.State.SkillElement
{
    public class SlipOutputState : PrimeSkillOutputState<SlipValueObject>
    {
        private readonly PrimeSkillStopState<SlipValueObject> _primeSkillStopState;
        private readonly SlipOutputFacade _slipOutput;

        public SlipOutputState(
            PrimeSkillStopState<SlipValueObject> primeSkillStopState,
            SlipOutputFacade slipOutput)
        {
            _primeSkillStopState = primeSkillStopState;
            _slipOutput = slipOutput;
        }

        public override async void Start()
        {
            var isFailure = Context.BattleEventQueue.All(x => x.IsFailure);
            if (isFailure)
            {
                await _slipOutput.OutputThenSlipFailureAsync();
                Context.BattleEventQueue.Clear();
            }
            else
            {
                var primeSkill = Context.BattleEventQueue.Dequeue();
                await _slipOutput.OutputThenSlipSuccessAsync(primeSkill);
            }
        }

        public override void Select()
        {
            BaseState<SlipValueObject> nextState = Context.BattleEventQueue.Count == 0
                ? _primeSkillStopState
                : this;
            Context.TransitionTo(nextState);
        }
    }
}