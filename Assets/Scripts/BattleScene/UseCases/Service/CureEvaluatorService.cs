using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class CureEvaluatorService
    {
        private readonly CharacterPropertyFactoryService _characterPropertyFactory;
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly IMyRandomService _myRandom;

        public CureEvaluatorService(
            CharacterPropertyFactoryService characterPropertyFactory,
            ICollection<CharacterEntity, CharacterId> characterCollection,
            IMyRandomService myRandom)
        {
            _characterPropertyFactory = characterPropertyFactory;
            _characterCollection = characterCollection;
            _myRandom = myRandom;
        }

        public int Evaluate(CharacterId actorId, CureValueObject cure)
        {
            return cure.CureExpressionCode switch
            {
                CureExpressionCode.Basic => BasicEvaluate(actorId, cure.Rate),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private int BasicEvaluate(CharacterId actorId, float rate)
        {
            var wisdom = _characterPropertyFactory.Create(actorId).Wisdom;
            var restore = (int)(wisdom * 8 * rate) + _myRandom.Range(0, 2);
            var currentHitPoint = _characterCollection.Get(actorId).CurrentHitPoint;
            var maxHitPoint = _characterPropertyFactory.Create(actorId).HitPoint;
            var actualRestore = Math.Min(restore, maxHitPoint - currentHitPoint);
            return actualRestore;
        }
    }
}