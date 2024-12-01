using System.Linq;
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
            var isFailure = Context.BattleEventQueue.All(x => x.IsFailure);
            if (isFailure)
            {
                _facade.OutputThenDestroyFailure();
                Context.BattleEventQueue.Clear();
            }
            else
            {
                var primeSkill = Context.BattleEventQueue.Dequeue();
                _facade.OutputThenDestroySuccess(primeSkill);
            }
        }

        public override void Select()
        {
            Context.TransitionTo(_skillElementStopState);
        }
    }
}