using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Service
{
    public class CharacterPropertyFactoryService
    {
        private readonly IFactory<CharacterPropertyValueObject, CharacterTypeCode> _characterPropertyFactory;
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;

        public CharacterPropertyFactoryService(
            IFactory<CharacterPropertyValueObject, CharacterTypeCode> characterPropertyFactory,
            ICollection<CharacterEntity, CharacterId> characterCollection)
        {
            _characterPropertyFactory = characterPropertyFactory;
            _characterCollection = characterCollection;
        }

        public CharacterPropertyValueObject Create(CharacterId characterId)
        {
            var characterTypeCode = _characterCollection.Get(characterId).CharacterTypeCode;
            var characterProperty = _characterPropertyFactory.Create(characterTypeCode);
            return characterProperty;
        }
    }
}