using System;
using BattleScene.UseCases.Event;
using BattleScene.UseCases.StateMachine;
using VContainer;

namespace BattleScene.UseCases
{
    public class EventExecutor
    {
        private readonly IObjectResolver _container;

        public EventExecutor(IObjectResolver container)
        {
            _container = container;
        }

        public StateCode Execute(StateCode stateCode)
        {
            return stateCode switch
            {
                StateCode.Initialize => _container.Resolve<InitializationEvent>().GetStateCode(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}