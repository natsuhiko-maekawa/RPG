using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Facade;

namespace BattleScene.InterfaceAdapter.State.PrimeSkill
{
    public class AilmentOutputState : PrimeSkillOutputState<AilmentValueObject>
    {
        private readonly AilmentOutputFacade _ailmentOutput;
        private readonly PrimeSkillStopState<AilmentValueObject> _primeSkillStopState;

        public AilmentOutputState(
            AilmentOutputFacade ailmentOutput,
            PrimeSkillStopState<AilmentValueObject> primeSkillStopState)
        {
            _ailmentOutput = ailmentOutput;
            _primeSkillStopState = primeSkillStopState;
        }

        public override async void Start()
        {
            var isFailure = Context.BattleEventQueue.Count == 0;
            if (isFailure)
            {
                await _ailmentOutput.OutputThenAilmentFailureAsync();
            }
            else
            {
                var ailment = Context.BattleEventQueue.Dequeue();
                await _ailmentOutput.OutputThenAilmentSuccessAsync(ailment);
            }
        }

        public override void Select()
        {
            Context.TransitionTo(_primeSkillStopState);
        }
    }
}