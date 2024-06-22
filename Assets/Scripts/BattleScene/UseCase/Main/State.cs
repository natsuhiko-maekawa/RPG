using System;
using System.Collections.Generic;
using BattleScene.UseCase.BusinessLogic.Interface;
using BattleScene.UseCase.Output.Interface;

namespace BattleScene.UseCase.Main
{
    internal class State
    {
        private readonly Event _startEvent;
        private readonly Event _updateEvent;
        private readonly Dictionary<Func<bool>, StateCode> _triggerDict;

        public State(
            Dictionary<Func<bool>, StateCode> triggerDict,
            Event startEvent = null,
            Event updateEvent = null)
        {
            _triggerDict = triggerDict;
            _startEvent = startEvent;
            _updateEvent = updateEvent;
        }
        
        public void Start()
        {
            Execute(_startEvent);
        }

        public void Update()
        {
            Execute(_updateEvent);
        }

        public void Stop()
        {
            
        }

        private void Execute(Event @event)
        {
            if (@event?.UseCaseList != null)
                foreach (var useCase in @event.UseCaseList)
                    useCase.Execute();
            
            if (@event?.OutputList != null)
                foreach (var output in @event.OutputList)
                    output.Out();
        }

        public StateCode Triggers()
        {
            foreach (var trigger in _triggerDict)
            {
                if (!trigger.Key.Invoke()) continue;
                return trigger.Value;
            }

            return StateCode.NoTrigger;
        }

        public record Event(
            List<IUseCase> UseCaseList = null,
            List<IOutput> OutputList = null);
    }
}