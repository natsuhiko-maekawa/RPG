using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.Event.Runner;
using BattleScene.UseCase.Service;
using static BattleScene.Domain.Code.CharacterTypeId;

namespace BattleScene.UseCase.Event
{
    internal class BattleStartEvent : IEvent
    {
        private readonly ActionTimeCreatorService _actionTimeCreator;
        private readonly IActionTimeRepository _actionTimeRepository;
        private readonly CharacterCreatorService _characterCreator;
        private readonly ICharacterRepository _characterRepository;
        private readonly CharactersDomainService _characters;

        public BattleStartEvent(
            ActionTimeCreatorService actionTimeCreator,
            CharacterCreatorService characterCreator,
            CharactersDomainService characters,
            IActionTimeRepository actionTimeRepository,
            ICharacterRepository characterRepository)
        {
            _actionTimeCreator = actionTimeCreator;
            _characterCreator = characterCreator;
            _characters = characters;
            _actionTimeRepository = actionTimeRepository;
            _characterRepository = characterRepository;
        }

        public EventCode Run()
        {
            var enemyTypeIdList = new List<CharacterTypeId> { Bee, Dragon, Mantis, Shuten, Slime };
            var enemyList = _characterCreator.CreateEnemyList(enemyTypeIdList);
            _characterRepository.Update(enemyList);

            var actionTimeList = _actionTimeCreator.CreateDefault(_characters.GetIdList());
            _actionTimeRepository.Update(actionTimeList);

            return EventCode.OrderDecisionEvent;
        }
    }
}