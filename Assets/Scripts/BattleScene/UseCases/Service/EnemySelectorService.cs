using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using Common;
using Utility;

namespace BattleScene.UseCases.Service
{
    public class EnemySelectorService : IEnemySelectorService
    {
        private readonly IFactory<CharacterPropertyValueObject, CharacterTypeCode> _characterPropertyFactory;
        private readonly IMyRandomService _myRandom;

        public EnemySelectorService(
            IFactory<CharacterPropertyValueObject, CharacterTypeCode> characterPropertyFactory,
            IMyRandomService myRandom)
        {
            _characterPropertyFactory = characterPropertyFactory;
            _myRandom = myRandom;
        }

        public void SelectEnemy(CharacterTypeCode[] options, List<CharacterEntity> selected)
        {
            selected.Clear();
            var playerSumParameter = _characterPropertyFactory.Create(CharacterTypeCode.Player).SumParameter;
            var combination = options
                .Select(characterTypeCode => _characterPropertyFactory.Create(characterTypeCode))
                .Combination(1, Constant.MaxEnemyCount)
                .Where(combination =>
                {
                    var diff
                        = playerSumParameter - combination.Sum(characterProperty => characterProperty.SumParameter);
                    return diff is >= 0 and <= 5;
                })
                .Select(combination => combination
                    .Select(characterProperty => characterProperty))
                .ToArray();

            var characterArray = _myRandom.Choice(combination)
                .Select((characterProperty, index) =>
                {
                    var characterId = new CharacterId();
                    return new CharacterEntity(
                        id: characterId,
                        characterTypeCode: characterProperty.CharacterTypeCode,
                        currentHitPoint: characterProperty.HitPoint,
                        position: index);
                })
                .ToArray();
            selected.AddRange(characterArray);
        }
    }
}