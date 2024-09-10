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
        private readonly CharacterPropertyFactoryService _characterPropertyFactory;
        private readonly IRepository<BuffEntity, BuffId> _buffRepository;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly OrderedItemsDomainService _orderedItems;

        public ActionTimeService(
            CharacterPropertyFactoryService characterPropertyFactory,
            IRepository<BuffEntity, BuffId> buffRepository,
            IRepository<CharacterEntity, CharacterId> characterRepository,
            OrderedItemsDomainService orderedItems)
        {
            _characterPropertyFactory = characterPropertyFactory;
            _buffRepository = buffRepository;
            _characterRepository = characterRepository;
            _orderedItems = orderedItems;
        }

        public void Update(IList<CharacterId> characterIdList)
        {
            if (!_orderedItems.First().TryGetCharacterId(out var actorId))
                return;

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
            var speed = (float)_characterPropertyFactory.Crate(characterId).Agility;
            if (_buffRepository.Select()
                    .Count(x => Equals(x.CharacterId, characterId)) != 0)
            {
                var buffId = new BuffId(characterId, BuffCode.Speed);
                speed *= _buffRepository.Select(buffId)
                    .Rate;
            }
            
            return (int)Math.Ceiling(speed);
        }
    }
}