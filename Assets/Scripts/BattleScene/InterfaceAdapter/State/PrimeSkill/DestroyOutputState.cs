using System.Linq;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Facade;

namespace BattleScene.InterfaceAdapter.State.PrimeSkill
{
    public class DestroyOutputState : PrimeSkillOutputState<DestroyValueObject>
    {
        private readonly DestroyOutputFacade _destroyOutput;
        private readonly PrimeSkillStopState<DestroyValueObject> _primeSkillStopState;

        public DestroyOutputState(
            DestroyOutputFacade destroyOutput,
            PrimeSkillStopState<DestroyValueObject> primeSkillStopState)
        {
            _destroyOutput = destroyOutput;
            _primeSkillStopState = primeSkillStopState;
        }

        public override async void Start()
        {
            var isFailure = Context.BattleEventQueue.All(x => x.IsFailure);
            if (isFailure)
            {
                await _destroyOutput.OutputThenDestroyFailureAsync();
                Context.BattleEventQueue.Clear();
            }
            else
            {
                var primeSkill = Context.BattleEventQueue.Dequeue();
                await _destroyOutput.OutputThenDestroySuccessAsync(primeSkill);
            }
        }

        public override void Select()
        {
            Context.TransitionTo(_primeSkillStopState);
        }
    }
}