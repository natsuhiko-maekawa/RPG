using System;
using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;

namespace BattleScene.UseCases.OldEvent
{
    public class ResetSkillViewOldEvent : IOldEvent, IWait
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