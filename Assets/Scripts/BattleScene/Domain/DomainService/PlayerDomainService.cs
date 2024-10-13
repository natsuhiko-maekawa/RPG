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
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;

        public PlayerDomainService(
            IFactory<CharacterPropertyValueObject, CharacterTypeCode> propertyFactory,
            IFactory<PlayerPropertyValueObject, CharacterTypeCode> playerPropertyFactory,
            ICollection<CharacterEntity, CharacterId> characterCollection
            )
        {
            _propertyFactory = propertyFactory;
            _playerPropertyFactory = playerPropertyFactory;
            _characterCollection = characterCollection;
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
            
            _characterCollection.Add(player);
        }
        
        public CharacterEntity Get()
        {
            return _characterCollection.Get().First(x => x.CharacterTypeCode == CharacterTypeCode.Player);
        }

        public CharacterId GetId()
        {
            return Get().Id;
        }
    }
}