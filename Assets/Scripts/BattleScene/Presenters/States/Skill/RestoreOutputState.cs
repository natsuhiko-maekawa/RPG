using BattleScene.Domain.ValueObjects;
using BattleScene.Presenters.PresenterFacades;

namespace BattleScene.Presenters.States.Skill
{
    public class RestoreOutputState : SkillOutputState<RestoreValueObject>
    {
        private readonly RestoreOutputPresenterFacade _facade;
        private readonly SkillStopState<RestoreValueObject> _skillStopState;

        public RestoreOutputState(
            RestoreOutputPresenterFacade facade,
            SkillStopState<RestoreValueObject> skillStopState)
        {
            _facade = facade;
            _skillStopState = skillStopState;
        }

        public override void Start()
        {
            _facade.Output();
        }

        public override void Select()
        {
            Context.TransitionTo(_skillStopState);
        }
    }
}