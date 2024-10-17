using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Facade;

namespace BattleScene.InterfaceAdapter.State.PrimeSkill
{
    public class AilmentOutputState : PrimeSkillOutputState<AilmentParameterValueObject, AilmentValueObject>
    {
        private readonly AilmentOutputFacade _ailmentOutput;
        private readonly PrimeSkillStopState<AilmentParameterValueObject, AilmentValueObject> _primeSkillStopState;

        public AilmentOutputState(
            AilmentOutputFacade ailmentOutput,
            PrimeSkillStopState<AilmentParameterValueObject, AilmentValueObject> primeSkillStopState)
        {
            _ailmentOutput = ailmentOutput;
            _primeSkillStopState = primeSkillStopState;
        }

        public override async void Start()
        {
            var isFailure = Context.PrimeSkillQueue.Count == 0;
            if (isFailure)
            {
                await _ailmentOutput.OutputThenAilmentFailureAsync();
            }
            else
            {
                var ailment = Context.PrimeSkillQueue.Dequeue();
                await _ailmentOutput.OutputThenAilmentSuccessAsync(ailment);
            }
        }

        public override void Select()
        {
            Context.TransitionTo(_primeSkillStopState);
        }
    }
}