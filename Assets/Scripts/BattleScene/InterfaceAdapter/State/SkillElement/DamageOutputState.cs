using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Facade;

namespace BattleScene.InterfaceAdapter.State.SkillElement
{
    public class DamageOutputState : SkillElementOutputState<DamageValueObject>
    {
        private readonly DamageOutputFacade _damageOutput;
        private readonly SkillElementStopState<DamageValueObject> _skillElementStopState;
        private readonly SkillElementBreakState<DamageValueObject> _skillElementBreakState;

        public DamageOutputState(
            DamageOutputFacade damageOutput,
            SkillElementStopState<DamageValueObject> skillElementStopState,
            SkillElementBreakState<DamageValueObject> skillElementBreakState)
        {
            _damageOutput = damageOutput;
            _skillElementStopState = skillElementStopState;
            _skillElementBreakState = skillElementBreakState;
        }

        public override async void Start()
        {
            await _damageOutput.Output(Context.BattleEventQueue.Peek());
        }

        public override void Select()
        {
            BaseState<DamageValueObject> nextState = Context.BattleEventQueue.Peek().IsAvoid
                ? _skillElementBreakState
                : _skillElementStopState;
            Context.TransitionTo(nextState);
        }
    }
}