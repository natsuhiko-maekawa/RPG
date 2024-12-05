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
            if (!_orderedItems.First().TryGetCharacterId(out var actorId)) return;
            var actionTime = _characterRepository.Get()
                .Min(x => x.ActionTime);
            foreach (var character in _characterRepository.Get())
            {
                character.ActionTime -= actionTime;

                if (character.Id != actorId) continue;
                var maxAgility = _battlePropertyFactory.Create().MaxAgility;
                var speed = _speed.GetSpeed(character.Id);
                character.ActionTime += maxAgility / speed;
            }
        }
    }
}