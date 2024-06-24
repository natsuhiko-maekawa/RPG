using System;
using BattleScene.UseCases.Event.Interface;
using BattleScene.UseCases.Event.Runner;

namespace BattleScene.UseCases.Event
{
    public class ResetSkillViewEvent : IEvent, IWait
    {
        public EventCode Run()
        {
            throw new NotImplementedException();
        }

        public EventCode NextEvent()
        {
            return EventCode.SwitchSkillEvent;
        }
    }
}