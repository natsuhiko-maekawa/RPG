using System.Linq;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Facade;

namespace BattleScene.InterfaceAdapter.State.SkillElement
{
    public class SlipOutputState : SkillElementOutputState<SlipValueObject>
    {
        private readonly SkillElementStopState<SlipValueObject> _skillElementStopState;
        private readonly SlipOutputFacade _slipOutput;

        public SlipOutputState(
            SkillElementStopState<SlipValueObject> skillElementStopState,
            SlipOutputFacade slipOutput)
        {
            _skillElementStopState = skillElementStopState;
            _slipOutput = slipOutput;
        }

        public override async void Start()
        {
            var isFailure = Context.BattleEventQueue.All(x => x.IsFailure);
            if (isFailure)
            {
                _slipOutput.OutputThenSlipFailureAsync();
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
                ? _skillElementStopState
                : this;
            Context.TransitionTo(nextState);
        }
    }
}