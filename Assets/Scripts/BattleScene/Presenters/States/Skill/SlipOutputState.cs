using BattleScene.Domain.ValueObjects;
using BattleScene.Presenters.PresenterFacades;

namespace BattleScene.Presenters.States.Skill
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
                _facade.OutputWhenSlipSuccess(successSlipEvent);
            }
            else
            {
                _facade.OutputWhenSlipFailure();
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