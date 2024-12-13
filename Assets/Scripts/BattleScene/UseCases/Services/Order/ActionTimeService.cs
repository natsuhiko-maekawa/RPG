using System.Linq;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.DomainServices;
using BattleScene.Domain.Entities;
using BattleScene.Domain.Ids;
using BattleScene.Domain.ValueObjects;
using BattleScene.UseCases.IServices;

namespace BattleScene.UseCases.Services.Order
{
    public class ActionTimeService
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly IFactory<BattlePropertyValueObject> _battlePropertyFactory;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ISpeedService _speed;

        public ActionTimeService(
            IRepository<CharacterEntity, CharacterId> characterRepository,
            IFactory<BattlePropertyValueObject> battlePropertyFactory,
            OrderedItemsDomainService orderedItems,
            ISpeedService speed)
        {
            _characterRepository = characterRepository;
            _battlePropertyFactory = battlePropertyFactory;
            _orderedItems = orderedItems;
            _speed = speed;
        }

        public void Update()
        {
            if (!_orderedItems.First().TryGetActor(out var actor)) return;
            var actionTime = _characterRepository.Get()
                .Min(x => x.ActionTime);
            foreach (var character in _characterRepository.Get())
            {
                character.ActionTime -= actionTime;

                if (character.Id != actor.Id) continue;
                var maxAgility = _battlePropertyFactory.Create().MaxAgility;
                var speed = _speed.GetSpeed(character.Id);
                character.ActionTime += maxAgility / speed;
            }
        }
    }
}