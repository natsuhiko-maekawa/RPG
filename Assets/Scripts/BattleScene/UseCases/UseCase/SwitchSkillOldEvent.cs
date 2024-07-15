using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;

namespace BattleScene.UseCases.UseCase
{
    public class SwitchSkillOldEvent : IOldEvent
    {
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ISkillRepository _skillRepository;

        public SwitchSkillOldEvent(OrderedItemsDomainService orderedItems, ISkillRepository skillRepository)
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