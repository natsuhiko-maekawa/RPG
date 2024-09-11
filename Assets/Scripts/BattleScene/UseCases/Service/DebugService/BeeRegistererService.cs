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
    public class BeeRegistererService : IEnemiesRegistererService
    {
        private readonly IFactory<PropertyValueObject, CharacterTypeCode> _propertyFactory;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly IRepository<EnemyEntity, CharacterId> _enemyRepository;

        public BeeRegistererService(
            IFactory<PropertyValueObject, CharacterTypeCode> propertyFactory,
            IRepository<CharacterEntity, CharacterId> characterRepository,
            IRepository<EnemyEntity, CharacterId> enemyRepository)
        {
            _propertyFactory = propertyFactory;
            _characterRepository = characterRepository;
            _enemyRepository = enemyRepository;
        }

        public void Register(IList<CharacterTypeCode> characterTypeIdList)
        {
            var characterList = Enumerable.Repeat(CharacterTypeCode.Bee, 4)
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
            
            var enemyList = characterList
                .OrderBy(x => x.CharacterTypeCode)
                .Select((x, i) => new EnemyEntity(x.Id, i))
                .ToImmutableList();
            _enemyRepository.Update(enemyList);
        }
    }
}