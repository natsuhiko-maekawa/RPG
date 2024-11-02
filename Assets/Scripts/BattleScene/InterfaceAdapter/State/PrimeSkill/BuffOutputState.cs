using System;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Facade;

namespace BattleScene.InterfaceAdapter.State.PrimeSkill
{
    public class BuffOutputState : PrimeSkillOutputState<BuffParameterValueObject>
    {
        private readonly BuffOutput _output;
        private readonly PrimeSkillStopState<BuffParameterValueObject> _primeSkillStopState;

        public BuffOutputState(
            PrimeSkillStopState<BuffParameterValueObject> primeSkillStopState,
            BuffOutput output)
        {
            _primeSkillStopState = primeSkillStopState;
            _output = output;
        }

        public override async void Start()
        {
            if (Context.BattleEventQueue.Count == 0)
                throw new InvalidOperationException(ExceptionMessage.ContextPrimeSkillQueueIsEmpty);
            var buff = Context.BattleEventQueue.Dequeue();
            await _output.Out(buff);
        }

        public override void Select()
        {
            Context.TransitionTo(_primeSkillStopState);
        }
    }
}