using System.Collections.Generic;
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
        private readonly IFactory<CharacterPropertyValueObject, CharacterTypeCode> _propertyFactory;
        private readonly IMyRandomService _myRandom;
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;

        public EnemiesRegistererService(
            IFactory<CharacterPropertyValueObject, CharacterTypeCode> propertyFactory,
            IMyRandomService myRandom,
            ICollection<CharacterEntity, CharacterId> characterCollection)
        {
            _propertyFactory = propertyFactory;
            _myRandom = myRandom;
            _characterCollection = characterCollection;
        }

        public void Register(IReadOnlyList<CharacterTypeCode> characterTypeIdList)
        {
            var options = characterTypeIdList
                .Select(x =>
                {
                    var property = _propertyFactory.Create(x);
                    return (Id: property.CharacterTypeCode, Parameter: property.SumParameter);
                })
                .ToList()
                .Combination(1, 4)
                .Where(x =>
                {
                    var diff
                        = _propertyFactory.Create(CharacterTypeCode.Player).SumParameter - x.Sum(y => y.Parameter);
                    return diff is >= 0 and <= 5;
                })
                .Select(x => x
                    .Select(y => y.Id)
                    .ToList())
                .ToList();

            var characterList = _myRandom.Choice(options)
                .Select((x, i) =>
                {
                    CharacterPropertyValueObject characterProperty = _propertyFactory.Create(x);
                    var characterId = new CharacterId();
                    return new CharacterEntity(
                        id: characterId,
                        characterTypeCode: characterProperty.CharacterTypeCode,
                        currentHitPoint: characterProperty.HitPoint,
                        position: i);
                })
                .ToList();
            _characterCollection.Add(characterList);
        }
    }
}