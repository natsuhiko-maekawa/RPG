using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.EventRunner;
using BattleScene.UseCase.Skill.AbstractClass;

namespace BattleScene.UseCase.Event
{
    public class SwitchSkillEvent : IEvent
    {
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ISkillRepository _skillRepository;
        
        public EventCode Run()
        {
            var characterId = _orderedItems.FirstCharacterId();
            var skill = _skillRepository.Select(characterId);
            return skill.FirstSkillService() switch
            {
                DamageSkillElement => EventCode.PlayerDamageEvent,
                AilmentSkillElement => EventCode.AilmentEvent,
                DestroyPartSkillElement => EventCode.DestroyedPartEvent,
                CureSkillElement => EventCode.CureEvent,
                ResetSkillElement => EventCode.ResetEvent,
                BuffSkillElement => EventCode.BuffEvent,
                _ => EventCode.LoopEndEvent
            };
        }
    }
}