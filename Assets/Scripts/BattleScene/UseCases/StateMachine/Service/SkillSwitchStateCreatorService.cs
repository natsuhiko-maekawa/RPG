using System;
using System.Collections.Generic;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.StateMachine.Service
{
    internal class SkillSwitchStateCreatorService
    {
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ISkillRepository _skillRepository;

        public State Create()
        {
            var characterId = _orderedItems.FirstCharacterId();
            var skill = _skillRepository.Select(characterId).FirstSkillService();

            var triggerDict = new Dictionary<Func<bool>, StateCode>
            {
                { () => skill is DamageSkillElement, StateCode.Damage },
                { () => skill is AilmentSkillElement, StateCode.Ailment },
                { () => skill is DestroyPartSkillElement, StateCode.DestroyedPart },
                { () => skill is CureSkillElement, StateCode.Cure },
                { () => skill is ResetSkillElement, StateCode.Reset },
                { () => skill is BuffSkillElement, StateCode.Buff }
            };

            return new State(
                triggerDict: triggerDict);
        }
    }
}