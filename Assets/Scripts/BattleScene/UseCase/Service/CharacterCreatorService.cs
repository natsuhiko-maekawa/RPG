using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.ValueObject;
using Utility;
using Utility.Interface;
using static BattleScene.Domain.Code.CharacterTypeId;

namespace BattleScene.UseCase.Service
{
    public class CharacterCreatorService
    {
        private readonly IPropertyFactory _propertyFactory;
        private readonly IRandomEx _randomEx;

        public CharacterCreatorService(IPropertyFactory propertyFactory, IRandomEx randomEx)
        {
            _propertyFactory = propertyFactory;
            _randomEx = randomEx;
        }

        public CharacterAggregate CreatePlayer()
        {
            return Create(_propertyFactory.Get(Girl));
        }

        public ImmutableList<CharacterAggregate> CreateEnemyList(IList<CharacterTypeId> characterTypeIdList)
        {
            var options = _propertyFactory.Get(characterTypeIdList)
                // TODO タプルに名前を付ける
                .Select(x => (Id: x.CharacterTypeId, SumParameter(x)))
                .Combination(1, 4)
                .Where(x =>
                {
                    var diff = SumParameter(_propertyFactory.Get(Girl)) - x.Sum(y => y.Item2);
                    return diff is >= 0 and <= 5;
                })
                .Select(x => x
                    .Select(y => y.Id)
                    .ToList())
                .ToList();

            return _randomEx.Choice(options)
                .Select(x => Create(_propertyFactory.Get(x)))
                .ToImmutableList();
        }

        private int SumParameter(PropertyValueObject v)
        {
            return (int)(v.HitPoint / 10.0f
                         * (v.Strength
                            + v.Vitality
                            + v.Intelligence
                            + v.Agility
                            + v.Luck)) / 10;
        }

        private CharacterAggregate Create(PropertyValueObject property)
        {
            var characterId = new CharacterId();
            return new CharacterAggregate(characterId, property);
        }
    }
}