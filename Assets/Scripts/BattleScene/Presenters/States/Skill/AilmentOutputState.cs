using BattleScene.Domain.ValueObjects;
using BattleScene.Presenters.PresenterFacades;

namespace BattleScene.Presenters.States.Skill
{
    public class AilmentOutputState : SkillElementOutputState<AilmentValueObject>
    {
        private readonly AilmentOutputPresenterFacade _facade;
        private readonly SkillElementStopState<AilmentValueObject> _skillElementStopState;

        public AilmentOutputState(
            AilmentOutputPresenterFacade facade,
            SkillElementStopState<AilmentValueObject> skillElementStopState)
        {
            _facade = facade;
            _skillElementStopState = skillElementStopState;
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
            Context.TransitionTo(_skillElementStopState);
        }
    }
}