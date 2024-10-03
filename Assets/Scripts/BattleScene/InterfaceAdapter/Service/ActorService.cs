﻿using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.UseCases.IService;

namespace BattleScene.InterfaceAdapter.Service
{
    public class ActorService
    {
        private readonly AilmentDomainService _ailment;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IMyRandomService _myRandom;

        public ActorService(
            AilmentDomainService ailment,
            IRepository<CharacterEntity, CharacterId> characterRepository,
            OrderedItemsDomainService orderedItems,
            IMyRandomService myRandom)
        {
            _ailment = ailment;
            _characterRepository = characterRepository;
            _orderedItems = orderedItems;
            _myRandom = myRandom;
        }

        public bool IsResetAilment => _orderedItems.First().OrderedItemType == OrderedItemType.Ailment;
        public bool IsSlipDamage => _orderedItems.First().OrderedItemType == OrderedItemType.Slip;
        public bool CantAction => CantCharacterAction();
        public bool IsPlayer
        {
            get
            {
                var isCharacter = _orderedItems.First().TryGetCharacterId(out var characterId);
                var isPlayer = isCharacter && _characterRepository.Select(characterId).IsPlayer;
                return isPlayer;
            }
        }
        
        private bool CantCharacterAction()
        {
            var actorIsCharacter = _orderedItems.First().TryGetCharacterId(out var characterId);
            if (!actorIsCharacter) return false;
            
            var ailmentCode = _ailment.GetHighestPriority(characterId)?.AilmentCode;
            if (!ailmentCode.HasValue) return false;

            var absoluteCantAction = ailmentCode.Value is not (AilmentCode.Paralysis or AilmentCode.EnemyParalysis);
            return absoluteCantAction || CantActionBecauseParalysis;
        }

        private bool CantActionBecauseParalysis => _myRandom.Probability(0.5f);
    }
}