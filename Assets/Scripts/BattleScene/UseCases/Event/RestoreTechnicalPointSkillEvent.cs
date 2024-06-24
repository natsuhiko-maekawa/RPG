using System;
using BattleScene.UseCases.Event.Interface;
using BattleScene.UseCases.Event.Runner;
using BattleScene.UseCases.Event.TemplateMethod;

namespace BattleScene.UseCases.Event
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