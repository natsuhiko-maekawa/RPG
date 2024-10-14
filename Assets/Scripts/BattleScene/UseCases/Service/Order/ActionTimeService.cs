using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service.Order
{
    public class ActionTimeService
    {
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly IFactory<BattlePropertyValueObject> _battlePropertyFactory;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ISpeedService _speed;

        public ActionTimeService(
            ICollection<CharacterEntity, CharacterId> characterCollection,
            IFactory<BattlePropertyValueObject> battlePropertyFactory,
            OrderedItemsDomainService orderedItems,
            ISpeedService speed)
        {
            _characterCollection = characterCollection;
            _battlePropertyFactory = battlePropertyFactory;
            _orderedItems = orderedItems;
            _speed = speed;
        }

        public void Update()
        {
            if (!_orderedItems.First().TryGetCharacterId(out var actorId)) return;

            var characterIdList = _characterCollection.Get().Select(x => x.Id).ToList();
            foreach (var character in _characterCollection.Get())
            {
                character.ActionTime = CreateActionTime(
                    characterIdList: characterIdList,
                    actorId: actorId,
                    characterId: character.Id);
            }
        }

        private int CreateActionTime(IReadOnlyList<CharacterId> characterIdList, CharacterId actorId, CharacterId characterId)
        {
            var actionTime = _characterCollection.Get(characterId).ActionTime;
            var minTime = characterIdList
                .Select(x => _characterCollection.Get(x).ActionTime)
                .Min();
            actionTime -= minTime;

            if (!Equals(characterId, actorId))
                return actionTime;

            actionTime += _battlePropertyFactory.Create().MaxAgility / _speed.Get(characterId);
            return actionTime;
        }
    }
}