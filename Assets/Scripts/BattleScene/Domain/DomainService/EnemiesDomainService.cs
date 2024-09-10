using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Aggregate;
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
        private readonly IRepository<ActionTimeEntity, CharacterId> _actionTimeRepository;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly IRepository<EnemyEntity, CharacterId> _enemyRepository;
        private readonly IRepository<HitPointAggregate, CharacterId> _hitPointRepository;

        public EnemiesDomainService(
            IFactory<PropertyValueObject, CharacterTypeCode> propertyFactory,
            IRandomEx randomEx, 
            IRepository<ActionTimeEntity, CharacterId> actionTimeRepository,
            IRepository<CharacterEntity, CharacterId> characterRepository,
            IRepository<EnemyEntity, CharacterId> enemyRepository, 
            IRepository<HitPointAggregate, CharacterId> hitPointRepository)
        {
            _propertyFactory = propertyFactory;
            _randomEx = randomEx;
            _actionTimeRepository = actionTimeRepository;
            _characterRepository = characterRepository;
            _enemyRepository = enemyRepository;
            _hitPointRepository = hitPointRepository;
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
                .Select(x =>
                {
                    PropertyValueObject property = _propertyFactory.Create(x);
                    var characterId = new CharacterId();
                    return new CharacterEntity(characterId, property);
                })
                .ToImmutableList();
            _characterRepository.Update(characterList);

            var hitPointList = characterList
                .Select(x => new HitPointAggregate(x.Id, x.Property.HitPoint))
                .ToImmutableList();
            _hitPointRepository.Update(hitPointList);
            
            var enemyList = characterList
                .OrderBy(x => x.CharacterTypeCode)
                .Select((x, i) => new EnemyEntity(x.Id, i))
                .ToImmutableList();
            _enemyRepository.Update(enemyList);
            
            var actionTimeList = characterList
                .Select(x => new ActionTimeEntity(x.Id))
                .ToImmutableList();
            _actionTimeRepository.Update(actionTimeList);
        }

        public ImmutableList<CharacterEntity> Get()
        {
            return _characterRepository.Select()
                .Where(x => !x.IsPlayer())
                .ToImmutableList();
        }
        
        public ImmutableList<CharacterId> GetIdSurvive()
        {
            return _characterRepository.Select()
                .Where(x => !x.IsPlayer())
                .Where(x => _hitPointRepository.Select(x.Id).IsSurvive())
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