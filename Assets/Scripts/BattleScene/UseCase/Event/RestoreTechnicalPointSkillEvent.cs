using System;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.Event.Runner;
using BattleScene.UseCase.Event.TemplateMethod;

namespace BattleScene.UseCase.Event
{
    public class RestoreTechnicalPointSkillEvent : SkillEvent, IEvent
    {
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ResultDomainService _result;
        private readonly IResultRepository _resultRepository;
        private readonly ISkillRepository _skillRepository;

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