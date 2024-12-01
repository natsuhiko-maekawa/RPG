using System.Linq;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.PresenterFacade;

namespace BattleScene.InterfaceAdapter.State.SkillElement
{
    public class SlipOutputState : SkillElementOutputState<SlipValueObject>
    {
        private readonly SkillElementStopState<SlipValueObject> _skillElementStopState;
        private readonly SlipOutputPresenterFacade _facade;

        public SlipOutputState(
            SkillElementStopState<SlipValueObject> skillElementStopState,
            SlipOutputPresenterFacade facade)
        {
            _skillElementStopState = skillElementStopState;
            _facade = facade;
        }

        public override void Start()
        {
            var isFailure = Context.BattleEventQueue.All(x => x.IsFailure);
            if (isFailure)
            {
                _facade.OutputThenSlipFailure();
                Context.BattleEventQueue.Clear();
            }
            else
            {
                var primeSkill = Context.BattleEventQueue.Dequeue();
                _facade.OutputThenSlipSuccess(primeSkill);
            }
        }

        public override void Select()
        {
            BaseState<SlipValueObject> nextState = Context.BattleEventQueue.Count == 0
                ? _skillElementStopState
                : this;
            Context.TransitionTo(nextState);
        }
    }
}