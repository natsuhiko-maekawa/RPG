using System;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Facade;

namespace BattleScene.InterfaceAdapter.State.SkillElement
{
    public class EnhanceOutputState: SkillElementOutputState<EnhanceValueObject>
    {
        private readonly EnhanceOutput _output;
        private readonly SkillElementStopState<EnhanceValueObject> _skillElementStopState;

        public EnhanceOutputState(
            EnhanceOutput output,
            SkillElementStopState<EnhanceValueObject> skillElementStopState)
        {
            _output = output;
            _skillElementStopState = skillElementStopState;
        }

        public override void Start()
        {
            if (Context.BattleEventQueue.Count == 0)
                throw new InvalidOperationException(ExceptionMessage.ContextBattleEventQueueIsEmpty);
            var enhance = Context.BattleEventQueue.Dequeue();
            _output.Out(enhance);
        }

        public override void Select()
        {
            Context.TransitionTo(_skillElementStopState);
        }
    }
}