using System;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.DomainService
{
    public class PlayerDomainService
    {
        private readonly IFactory<CharacterPropertyValueObject, CharacterTypeCode> _propertyFactory;
        private readonly IFactory<PlayerPropertyValueObject, CharacterTypeCode> _playerPropertyFactory;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;

        public PlayerDomainService(
            IFactory<CharacterPropertyValueObject, CharacterTypeCode> propertyFactory,
            IFactory<PlayerPropertyValueObject, CharacterTypeCode> playerPropertyFactory,
            IRepository<CharacterEntity, CharacterId> characterRepository
        )
        {
            _propertyFactory = propertyFactory;
            _playerPropertyFactory = playerPropertyFactory;
            _characterRepository = characterRepository;
        }

        public void Add()
        {
            var characterId = new CharacterId();
            CharacterPropertyValueObject characterProperty = _propertyFactory.Create(CharacterTypeCode.Player);
            var playerProperty = _playerPropertyFactory.Create(CharacterTypeCode.Player);

            var player = new CharacterEntity(
                id: characterId,
                characterTypeCode: characterProperty.CharacterTypeCode,
                currentHitPoint: characterProperty.HitPoint,
                currentTechnicalPoint: playerProperty.TechnicalPoint);

            _characterRepository.Add(player);
        }

        public CharacterEntity Get() => _characterRepository.Get()
                .Single(x => x.CharacterTypeCode == CharacterTypeCode.Player);
    }
}