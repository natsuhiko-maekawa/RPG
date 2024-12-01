using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Facade;

namespace BattleScene.InterfaceAdapter.State.SkillElement
{
    public class RestoreOutputState : SkillElementOutputState<RestoreValueObject>
    {
        private readonly RestoreOutputPresenterFacade _facade;
        private readonly SkillElementStopState<RestoreValueObject> _skillElementStopState;

        public RestoreOutputState(
            RestoreOutputPresenterFacade facade,
            SkillElementStopState<RestoreValueObject> skillElementStopState)
        {
            _facade = facade;
            _skillElementStopState = skillElementStopState;
        }

        public override void Start()
        {
            _facade.Output();
        }

        public override void Select()
        {
            Context.TransitionTo(_skillElementStopState);
        }
    }
}