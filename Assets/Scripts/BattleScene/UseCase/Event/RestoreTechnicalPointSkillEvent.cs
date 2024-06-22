using System;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.Event.Runner;
using BattleScene.UseCase.Event.TemplateMethod;

namespace BattleScene.UseCase.Event
{
    public class RestoreTechnicalPointSkillEvent : SkillEvent, IEvent
    {
        protected override void UpdateResultRepository()
        {
            throw new NotImplementedException();
        }

        protected override void UpdateSkillRepository()
        {
            throw new NotImplementedException();
        }

        protected override EventCode RunSkillEvent()
        {
            throw new NotImplementedException();
        }
    }
}