using BattleScene.Domain.ValueObjects;
using BattleScene.Presenters.PresenterFacades;

namespace BattleScene.Presenters.States.Skill
{
    public class CureOutputState : SkillOutputState<CureValueObject>
    {
        private readonly CureOutputPresenterFacade _facade;
        private readonly SkillStopState<CureValueObject> _skillStopState;

        public CureOutputState(
            CureOutputPresenterFacade facade,
            SkillStopState<CureValueObject> skillStopState)
        {
            _facade = facade;
            _skillStopState = skillStopState;
        }

        public override void Start()
        {
            _facade.Output(Context.BattleEventQueue.Peek());
        }

        public override void Select()
        {
            Context.TransitionTo(_skillStopState);
        }
    }
}