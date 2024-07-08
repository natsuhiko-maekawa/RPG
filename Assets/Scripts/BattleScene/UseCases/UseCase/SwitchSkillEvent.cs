using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.Event.Interface;
using BattleScene.UseCases.Event.Runner;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Event
{
    public class SwitchSkillEvent : IEvent
    {
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ISkillRepository _skillRepository;

        public SwitchSkillEvent(OrderedItemsDomainService orderedItems, ISkillRepository skillRepository)
        {
            _orderedItems = orderedItems;
            _skillRepository = skillRepository;
        }

        public EventCode Run()
        {
            var characterId = _orderedItems.FirstCharacterId();
            var skill = _skillRepository.Select(characterId);
            return skill.FirstSkillService() switch
            {
                AbstractDamage => EventCode.PlayerDamageEvent,
                AbstractAilment => EventCode.AilmentEvent,
                AbstractDestroyPart => EventCode.DestroyedPartEvent,
                AbstractCure => EventCode.CureEvent,
                AbstractReset => EventCode.ResetEvent,
                AbstractBuff => EventCode.BuffEvent,
                _ => EventCode.LoopEndEvent
            };
        }
    }
}