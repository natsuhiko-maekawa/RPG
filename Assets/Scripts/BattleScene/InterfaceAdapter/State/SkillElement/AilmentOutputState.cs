using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Facade;

namespace BattleScene.InterfaceAdapter.State.SkillElement
{
    public class AilmentOutputState : SkillElementOutputState<AilmentValueObject>
    {
        private readonly AilmentOutputFacade _ailmentOutput;
        private readonly SkillElementStopState<AilmentValueObject> _skillElementStopState;

        public AilmentOutputState(
            AilmentOutputFacade ailmentOutput,
            SkillElementStopState<AilmentValueObject> skillElementStopState)
        {
            _ailmentOutput = ailmentOutput;
            _skillElementStopState = skillElementStopState;
        }

        public override async void Start()
        {
            var isFailure = Context.BattleEventQueue.Count == 0;
            if (isFailure)
            {
                _ailmentOutput.OutputThenAilmentFailureAsync();
            }
            else
            {
                var ailment = Context.BattleEventQueue.Dequeue();
                await _ailmentOutput.OutputThenAilmentSuccessAsync(ailment);
            }
        }

        public override void Select()
        {
            Context.TransitionTo(_skillElementStopState);
        }
    }
}