using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service.Order
{
    public class ActionTimeService
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ISpeedService _speed;

        public ActionTimeService(
            IRepository<CharacterEntity, CharacterId> characterRepository,
            OrderedItemsDomainService orderedItems,
            ISpeedService speed)
        {
            _characterRepository = characterRepository;
            _orderedItems = orderedItems;
            _speed = speed;
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

            actionTime += Constant.MaxAgility / _speed.Get(characterId);
            return actionTime;
        }
    }
}