using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Facade;

namespace BattleScene.InterfaceAdapter.State.SkillElement
{
    public class CureOutputState : SkillElementOutputState<CureValueObject>
    {
        private readonly CureOutput _output;
        private readonly SkillElementStopState<CureValueObject> _skillElementStopState;

        public CureOutputState(
            CureOutput output,
            SkillElementStopState<CureValueObject> skillElementStopState)
        {
            _output = output;
            _skillElementStopState = skillElementStopState;
        }

        public override async void Start()
        {
            await _output.Output(Context.BattleEventQueue.Peek());
        }

        public override void Select()
        {
            Context.TransitionTo(_skillElementStopState);
        }
    }
}