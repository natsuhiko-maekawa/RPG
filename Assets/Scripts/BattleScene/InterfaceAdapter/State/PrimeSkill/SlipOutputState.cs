using System.Linq;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Facade;

namespace BattleScene.InterfaceAdapter.State.PrimeSkill
{
    public class SlipOutputState : PrimeSkillOutputState<SlipParameterValueObject, SlipValueObject>
    {
        private readonly PrimeSkillStopState<SlipParameterValueObject, SlipValueObject> _primeSkillStopState;
        private readonly SlipOutputFacade _slipOutput;

        public SlipOutputState(
            PrimeSkillStopState<SlipParameterValueObject, SlipValueObject> primeSkillStopState,
            SlipOutputFacade slipOutput)
        {
            _primeSkillStopState = primeSkillStopState;
            _slipOutput = slipOutput;
        }

        public override async void Start()
        {
            var isFailure = Context.PrimeSkillQueue.All(x => x.IsFailure);
            if (isFailure)
            {
                await _slipOutput.OutputThenSlipFailureAsync();
                Context.PrimeSkillQueue.Clear();
            }
            else
            {
                var primeSkill = Context.PrimeSkillQueue.Dequeue();
                await _slipOutput.OutputThenSlipSuccessAsync(primeSkill);
            }
        }
        
        public override void Select()
        {
            BaseState<SlipParameterValueObject, SlipValueObject> nextState = Context.PrimeSkillQueue.Count == 0 
                ? _primeSkillStopState 
                : this;
            Context.TransitionTo(nextState);
        }
    }
}