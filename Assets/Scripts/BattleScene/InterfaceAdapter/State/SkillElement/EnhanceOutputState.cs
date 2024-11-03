using System;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Facade;

namespace BattleScene.InterfaceAdapter.State.SkillElement
{
    public class EnhanceOutputState: PrimeSkillOutputState<EnhanceValueObject>
    {
        private readonly EnhanceOutput _output;
        private readonly PrimeSkillStopState<EnhanceValueObject> _primeSkillStopState;

        public EnhanceOutputState(
            EnhanceOutput output,
            PrimeSkillStopState<EnhanceValueObject> primeSkillStopState)
        {
            _output = output;
            _primeSkillStopState = primeSkillStopState;
        }

        public override async void Start()
        {
            if (Context.BattleEventQueue.Count == 0)
                throw new InvalidOperationException(ExceptionMessage.ContextPrimeSkillQueueIsEmpty);
            var enhance = Context.BattleEventQueue.Dequeue();
            await _output.Out(enhance);
        }

        public override void Select()
        {
            Context.TransitionTo(_primeSkillStopState);
        }
    }
}