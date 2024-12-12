using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.PresenterFacade;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class CureOutputState : SkillElementOutputState<CureValueObject>
    {
        private readonly CureOutputPresenterFacade _facade;
        private readonly SkillElementStopState<CureValueObject> _skillElementStopState;

        public CureOutputState(
            CureOutputPresenterFacade facade,
            SkillElementStopState<CureValueObject> skillElementStopState)
        {
            _facade = facade;
            _skillElementStopState = skillElementStopState;
        }

        public override void Start()
        {
            _facade.Output(Context.BattleEventQueue.Peek());
        }

        public override void Select()
        {
            Context.TransitionTo(_skillElementStopState);
        }
    }
}