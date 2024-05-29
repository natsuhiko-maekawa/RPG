using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.Event.TemplateMethod;
using BattleScene.UseCase.EventRunner;

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
            throw new System.NotImplementedException();
        }

        protected override void UpdateSkillRepository()
        {
            throw new System.NotImplementedException();
        }

        protected override EventCode RunSkillEvent()
        {
            throw new System.NotImplementedException();
        }
    }
}