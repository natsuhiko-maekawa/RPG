using System.Linq;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Facade;

namespace BattleScene.InterfaceAdapter.State.SkillElement
{
    public class DestroyOutputState : SkillElementOutputState<DestroyValueObject>
    {
        private readonly DestroyOutputFacade _destroyOutput;
        private readonly SkillElementStopState<DestroyValueObject> _skillElementStopState;

        public DestroyOutputState(
            DestroyOutputFacade destroyOutput,
            SkillElementStopState<DestroyValueObject> skillElementStopState)
        {
            _destroyOutput = destroyOutput;
            _skillElementStopState = skillElementStopState;
        }

        public override void Start()
        {
            var isFailure = Context.BattleEventQueue.All(x => x.IsFailure);
            if (isFailure)
            {
                _destroyOutput.OutputThenDestroyFailureAsync();
                Context.BattleEventQueue.Clear();
            }
            else
            {
                var primeSkill = Context.BattleEventQueue.Dequeue();
                _destroyOutput.OutputThenDestroySuccessAsync(primeSkill);
            }
        }

        public override void Select()
        {
            Context.TransitionTo(_skillElementStopState);
        }
    }
}