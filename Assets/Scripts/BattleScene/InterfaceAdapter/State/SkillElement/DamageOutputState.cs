using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.PresenterFacade;

namespace BattleScene.InterfaceAdapter.State.SkillElement
{
    public class DamageOutputState : SkillElementOutputState<DamageValueObject>
    {
        private readonly DamageOutputPresenterFacade _facade;
        private readonly SkillElementStopState<DamageValueObject> _skillElementStopState;
        private readonly SkillElementBreakState<DamageValueObject> _skillElementBreakState;

        public DamageOutputState(
            DamageOutputPresenterFacade facade,
            SkillElementStopState<DamageValueObject> skillElementStopState,
            SkillElementBreakState<DamageValueObject> skillElementBreakState)
        {
            _facade = facade;
            _skillElementStopState = skillElementStopState;
            _skillElementBreakState = skillElementBreakState;
        }

        public override void Start()
        {
            _facade.Output(Context.BattleEventQueue.Peek());
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