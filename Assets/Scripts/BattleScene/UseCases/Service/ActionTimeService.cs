using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;

namespace BattleScene.UseCases.Service
{
    public class ActionTimeService
    {
        private readonly BuffDomainService _buff;
        private readonly CharacterPropertyFactoryService _characterPropertyFactory;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly OrderedItemsDomainService _orderedItems;

        public ActionTimeService(
            BuffDomainService buff,
            CharacterPropertyFactoryService characterPropertyFactory,
            IRepository<CharacterEntity, CharacterId> characterRepository,
            OrderedItemsDomainService orderedItems)
        {
            _buff = buff;
            _characterPropertyFactory = characterPropertyFactory;
            _characterRepository = characterRepository;
            _orderedItems = orderedItems;
        }

        public void Update()
        {
            if (!_orderedItems.First().TryGetCharacterId(out var actorId))
                return;

            var characterIdList = _characterRepository.Select().Select(x => x.Id).ToImmutableList();
            var characterList = characterIdList
                .Select(x =>
                {
                    var character = _characterRepository.Select(x);
                    var actionTime = ComputeActionTime(
                        characterIdList: characterIdList,
                        actorId: actorId,
                        characterId: x);
                    character.ActionTime = actionTime;
                    return character;
                })
                .ToImmutableList();
            
            _characterRepository.Update(characterList);
        }

        private int ComputeActionTime(IList<CharacterId> characterIdList, CharacterId actorId, CharacterId characterId)
        {
            var actionTime = _characterRepository.Select(characterId).ActionTime;
            var minTime = characterIdList
                .Select(x => _characterRepository.Select(x).ActionTime)
                .Min();
            actionTime -= minTime;

            if (!Equals(characterId, actorId))
                return actionTime;

            actionTime += Constant.MaxAgility / GetSpeed(characterId);
            return actionTime;
        }
        
        private int GetSpeed(CharacterId characterId)
        {
            var agility = (float)_characterPropertyFactory.Crate(characterId).Agility;
            var speedRate = _buff.GetRate(characterId, BuffCode.Speed);
            var speed = (int)Math.Ceiling(agility * speedRate);
            
            return speed;
        }
    }
}