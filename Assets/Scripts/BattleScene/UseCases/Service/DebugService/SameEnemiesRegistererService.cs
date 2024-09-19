using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service.DebugService
{
    public class SameEnemiesRegistererService : IEnemiesRegistererService
    {
        private readonly IFactory<PropertyValueObject, CharacterTypeCode> _propertyFactory;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;

        public SameEnemiesRegistererService(
            IFactory<PropertyValueObject, CharacterTypeCode> propertyFactory,
            IRepository<CharacterEntity, CharacterId> characterRepository)
        {
            _propertyFactory = propertyFactory;
            _characterRepository = characterRepository;
        }

        public void Register(IList<CharacterTypeCode> characterTypeIdList)
        {
            var characterList = Enumerable.Repeat(CharacterTypeCode.Shuten, 4)
                .Select((x, i) =>
                {
                    PropertyValueObject property = _propertyFactory.Create(x);
                    var characterId = new CharacterId();
                    return new CharacterEntity(
                        id: characterId,
                        characterTypeCode: property.CharacterTypeCode,
                        currentHitPoint: property.HitPoint,
                        position: i);
                })
                .ToImmutableList();
            _characterRepository.Update(characterList);
        }
    }
}