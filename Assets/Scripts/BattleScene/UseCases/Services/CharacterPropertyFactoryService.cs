using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.Ids;
using BattleScene.Domain.ValueObjects;

namespace BattleScene.UseCases.Services
{
    public class CharacterPropertyFactoryService
    {
        private readonly IFactory<CharacterPropertyValueObject, CharacterTypeCode> _characterPropertyFactory;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;

        public CharacterPropertyFactoryService(
            IFactory<CharacterPropertyValueObject, CharacterTypeCode> characterPropertyFactory,
            IRepository<CharacterEntity, CharacterId> characterRepository)
        {
            _characterPropertyFactory = characterPropertyFactory;
            _characterRepository = characterRepository;
        }

        public CharacterPropertyValueObject Create(CharacterId characterId)
        {
            var characterTypeCode = _characterRepository.Get(characterId).CharacterTypeCode;
            var characterProperty = _characterPropertyFactory.Create(characterTypeCode);
            return characterProperty;
        }
    }
}