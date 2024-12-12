using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.PresenterFacades;

namespace BattleScene.InterfaceAdapter.States.Skill
{
    public class SlipOutputState : SkillElementOutputState<SlipValueObject>
    {
        private readonly SkillElementStopState<SlipValueObject> _skillElementStopState;
        private readonly SlipOutputPresenterFacade _facade;

        public SlipOutputState(
            SkillElementStopState<SlipValueObject> skillElementStopState,
            SlipOutputPresenterFacade facade)
        {
            _skillElementStopState = skillElementStopState;
            _facade = facade;
        }

        public override void Start()
        {
            if (TryGetSuccessBattleEvent(out var successSlipEvent))
            {
                _facade.OutputThenSlipSuccess(successSlipEvent);
            }
            else
            {
                _facade.OutputThenSlipFailure();
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