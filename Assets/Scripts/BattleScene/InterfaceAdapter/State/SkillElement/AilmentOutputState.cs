using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.PresenterFacade;

namespace BattleScene.InterfaceAdapter.State.SkillElement
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
                _facade.OutputThenAilmentSuccess(successAilmentEvent);
            }
            else
            {
                _facade.OutputThenAilmentFailure();
            }
        }

        public override void Select()
        {
            Context.TransitionTo(_skillElementStopState);
        }
    }
}