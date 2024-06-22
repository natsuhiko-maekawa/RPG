using System;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.Event.Runner;

namespace BattleScene.UseCase.Event
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