using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using Utility;
using Utility.Interface;

namespace BattleScene.Domain.DomainService
{
    public class EnemiesDomainService
    {
        private readonly IFactory<PropertyValueObject, CharacterTypeCode> _propertyFactory;
        private readonly IRandomEx _randomEx;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly IRepository<EnemyEntity, CharacterId> _enemyRepository;

        public EnemiesDomainService(
            IFactory<PropertyValueObject, CharacterTypeCode> propertyFactory,
            IRandomEx randomEx, 
            IRepository<CharacterEntity, CharacterId> characterRepository,
            IRepository<EnemyEntity, CharacterId> enemyRepository)
        {
            _propertyFactory = propertyFactory;
            _randomEx = randomEx;
            _characterRepository = characterRepository;
            _enemyRepository = enemyRepository;
        }

        public void Add(IList<CharacterTypeCode> characterTypeIdList)
        {
            var options = characterTypeIdList
                .Select(x =>
                {
                    var property = _propertyFactory.Create(x);
                    return (Id: property.CharacterTypeCode, Parameter: property.SumParameter());
                })
                .Combination(1, 4)
                .Where(x =>
                {
                    var diff 
                        = _propertyFactory.Create(CharacterTypeCode.Player).SumParameter() - x.Sum(y => y.Parameter);
                    return diff is >= 0 and <= 5;
                })
                .Select(x => x
                    .Select(y => y.Id)
                    .ToList())
                .ToList();
            
            var characterList = _randomEx.Choice(options)
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

        public ImmutableList<CharacterEntity> Get()
        {
            return _characterRepository.Select()
                .Where(x => x.CharacterTypeCode != CharacterTypeCode.Player)
                .ToImmutableList();
        }
        
        public ImmutableList<CharacterId> GetIdSurvive()
        {
            return _characterRepository.Select()
                .Where(x => x.CharacterTypeCode != CharacterTypeCode.Player)
                .Where(x => x.IsSurvive)
                .Select(x => x.Id)
                .ToImmutableList();
        }

        public CharacterId GetIdByPosition(int position)
        {
            return _enemyRepository.Select()
                .First(x => x.EnemyNumber == position)
                .Id;
        }
    }
}