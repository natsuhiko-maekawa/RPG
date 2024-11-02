using System.Linq;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Facade;

namespace BattleScene.InterfaceAdapter.State.PrimeSkill
{
    public class SlipOutputState : PrimeSkillOutputState<SlipParameterValueObject>
    {
        private readonly PrimeSkillStopState<SlipParameterValueObject> _primeSkillStopState;
        private readonly SlipOutputFacade _slipOutput;

        public SlipOutputState(
            PrimeSkillStopState<SlipParameterValueObject> primeSkillStopState,
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
            BaseState<SlipParameterValueObject> nextState = Context.BattleEventQueue.Count == 0
                ? _primeSkillStopState
                : this;
            Context.TransitionTo(nextState);
        }
    }
}