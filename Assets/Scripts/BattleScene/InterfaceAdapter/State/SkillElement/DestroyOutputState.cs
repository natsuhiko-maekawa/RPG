using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.PresenterFacade;

namespace BattleScene.InterfaceAdapter.State.SkillElement
{
    public class DestroyOutputState : SkillElementOutputState<DestroyValueObject>
    {
        private readonly DestroyOutputPresenterFacade _facade;
        private readonly SkillElementStopState<DestroyValueObject> _skillElementStopState;

        public DestroyOutputState(
            DestroyOutputPresenterFacade facade,
            SkillElementStopState<DestroyValueObject> skillElementStopState)
        {
            _facade = facade;
            _skillElementStopState = skillElementStopState;
        }

        public override void Start()
        {
            if (TryGetSuccessBattleEvent(out var successDestroyEvent))
            {
                _facade.OutputThenDestroySuccess(successDestroyEvent);
            }
            else
            {
                _facade.OutputThenDestroyFailure();
            }
        }

        public override void Select()
        {
            Context.TransitionTo(_skillElementStopState);
        }
    }
}