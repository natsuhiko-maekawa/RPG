using BattleScene.Domain.ValueObjects;
using BattleScene.Presenters.PresenterFacades;

namespace BattleScene.Presenters.States.Skill
{
    public class DestroyOutputState : SkillOutputState<DestroyValueObject>
    {
        private readonly DestroyOutputPresenterFacade _facade;
        private readonly SkillStopState<DestroyValueObject> _skillStopState;

        public DestroyOutputState(
            DestroyOutputPresenterFacade facade,
            SkillStopState<DestroyValueObject> skillStopState)
        {
            _facade = facade;
            _skillStopState = skillStopState;
        }

        public override void Start()
        {
            if (TryGetSuccessBattleEvent(out var successDestroyEvent))
            {
                _facade.OutputWhenDestroySuccess(successDestroyEvent);
            }
            else
            {
                _facade.OutputWhenDestroyFailure();
            }
        }

        public override void Select()
        {
            Context.TransitionTo(_skillStopState);
        }
    }
}