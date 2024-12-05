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