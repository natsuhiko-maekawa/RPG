using BattleScene.Domain.ValueObjects;
using BattleScene.Presenters.PresenterFacades;

namespace BattleScene.Presenters.States.Skill
{
    public class ResetOutputState : SkillOutputState<RecoveryValueObject>
    {
        private readonly ResetOutputPresenterFacade _facade;
        private readonly SkillStopState<RecoveryValueObject> _skillStopState;

        public ResetOutputState(
            ResetOutputPresenterFacade facade,
            SkillStopState<RecoveryValueObject> skillStopState)
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