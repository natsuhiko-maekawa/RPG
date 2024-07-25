using System;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;

namespace BattleScene.UseCases.StateMachine.Service
{
    internal class SkillSwitchStateCreatorService
    {
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ISkillRepository _skillRepository;

        public State Create()
        {
            // var characterId = _orderedItems.FirstCharacterId();
            // var skill = _skillRepository.Select(characterId).FirstSkillService();
            //
            // var triggerDict = new Dictionary<Func<bool>, StateCode>
            // {
            //     { () => skill is AbstractDamage, StateCode.Damage },
            //     { () => skill is AbstractAilment, StateCode.Ailment },
            //     { () => skill is AbstractDestroyPart, StateCode.DestroyedPart },
            //     { () => skill is AbstractCure, StateCode.Cure },
            //     { () => skill is AbstractReset, StateCode.Reset },
            //     { () => skill is AbstractBuff, StateCode.Buff }
            // };
            //
            // return new State(
            //     triggerDict: triggerDict);
            throw new NotImplementedException();
        }
    }
}