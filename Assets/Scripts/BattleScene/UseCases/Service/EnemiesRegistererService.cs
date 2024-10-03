using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using Utility;

namespace BattleScene.UseCases.Service
{
    public class EnemiesRegistererService : IEnemiesRegistererService
    {
        private readonly IFactory<PropertyValueObject, CharacterTypeCode> _propertyFactory;
        private readonly IMyRandomService _myRandom;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;

        public EnemiesRegistererService(
            IFactory<PropertyValueObject, CharacterTypeCode> propertyFactory,
            IMyRandomService myRandom, 
            IRepository<CharacterEntity, CharacterId> characterRepository)
        {
            _propertyFactory = propertyFactory;
            _myRandom = myRandom;
            _characterRepository = characterRepository;
        }

        public void Register(IList<CharacterTypeCode> characterTypeIdList)
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
            
            var characterList = _myRandom.Choice(options)
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