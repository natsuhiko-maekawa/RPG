using System;
using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;
using BattleScene.UseCases.OldEvent.TemplateMethod;

namespace BattleScene.UseCases.OldEvent
{
    public class RestoreTechnicalPointSkillOldEvent : SkillEvent, IOldEvent
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