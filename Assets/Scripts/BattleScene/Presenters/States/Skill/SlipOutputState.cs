using BattleScene.Domain.ValueObjects;
using BattleScene.Presenters.PresenterFacades;

namespace BattleScene.Presenters.States.Skill
{
    public class SlipOutputState : SkillOutputState<SlipValueObject>
    {
        private readonly SkillStopState<SlipValueObject> _skillStopState;
        private readonly SlipOutputPresenterFacade _facade;

        public SlipOutputState(
            SkillStopState<SlipValueObject> skillStopState,
            SlipOutputPresenterFacade facade)
        {
            _skillStopState = skillStopState;
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
                ? _skillStopState
                : this;
            Context.TransitionTo(nextState);
        }
    }
}