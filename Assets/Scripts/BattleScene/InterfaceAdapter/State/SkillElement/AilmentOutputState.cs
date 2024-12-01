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
            var isFailure = Context.BattleEventQueue.Count == 0;
            if (isFailure)
            {
                _facade.OutputThenAilmentFailure();
            }
            else
            {
                var ailment = Context.BattleEventQueue.Dequeue();
                _facade.OutputThenAilmentSuccess(ailment);
            }
        }

        public override void Select()
        {
            Context.TransitionTo(_skillElementStopState);
        }
    }
}