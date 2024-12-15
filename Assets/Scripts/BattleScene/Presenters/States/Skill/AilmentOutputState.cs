using BattleScene.Domain.ValueObjects;
using BattleScene.Presenters.PresenterFacades;

namespace BattleScene.Presenters.States.Skill
{
    public class AilmentOutputState : SkillOutputState<AilmentValueObject>
    {
        private readonly AilmentOutputPresenterFacade _facade;
        private readonly SkillStopState<AilmentValueObject> _skillStopState;

        public AilmentOutputState(
            AilmentOutputPresenterFacade facade,
            SkillStopState<AilmentValueObject> skillStopState)
        {
            _facade = facade;
            _skillStopState = skillStopState;
        }

        public override void Start()
        {
            if (TryGetSuccessBattleEvent(out var successAilmentEvent))
            {
                _facade.OutputWhenAilmentSuccess(successAilmentEvent);
            }
            else
            {
                _facade.OutputWhenAilmentFailure();
            }
        }

        public override void Select()
        {
            Context.TransitionTo(_skillStopState);
        }
    }
}