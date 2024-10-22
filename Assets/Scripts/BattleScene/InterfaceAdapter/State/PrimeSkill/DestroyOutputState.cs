using System.Linq;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Facade;

namespace BattleScene.InterfaceAdapter.State.PrimeSkill
{
    public class DestroyOutputState : PrimeSkillOutputState<DestroyParameterValueObject, PrimeSkillValueObject>
    {
        private readonly DestroyOutputFacade _destroyOutput;
        private readonly PrimeSkillStopState<DestroyParameterValueObject, PrimeSkillValueObject> _primeSkillStopState;

        public DestroyOutputState(
            DestroyOutputFacade destroyOutput,
            PrimeSkillStopState<DestroyParameterValueObject, PrimeSkillValueObject> primeSkillStopState)
        {
            _destroyOutput = destroyOutput;
            _primeSkillStopState = primeSkillStopState;
        }

        public override async void Start()
        {
            var isFailure = Context.PrimeSkillQueue.All(x => x.IsFailure);
            if (isFailure)
            {
                await _destroyOutput.OutputThenDestroyFailureAsync();
                Context.PrimeSkillQueue.Clear();
            }
            else
            {
                var primeSkill = Context.PrimeSkillQueue.Dequeue();
                await _destroyOutput.OutputThenDestroySuccessAsync(primeSkill);
            }
        }

        public override void Select()
        {
            Context.TransitionTo(_primeSkillStopState);
        }
    }
}