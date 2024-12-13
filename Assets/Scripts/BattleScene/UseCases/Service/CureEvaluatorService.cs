using System;
using BattleScene.Domain.Codes;
using BattleScene.Domain.Entities;
using BattleScene.Domain.ValueObjects;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class CureEvaluatorService
    {
        private readonly CharacterPropertyFactoryService _characterPropertyFactory;
        private readonly IMyRandomService _myRandom;

        public CureEvaluatorService(
            CharacterPropertyFactoryService characterPropertyFactory,
            IMyRandomService myRandom)
        {
            _characterPropertyFactory = characterPropertyFactory;
            _myRandom = myRandom;
        }

        public int Evaluate(CharacterEntity actor, CureValueObject cure)
        {
            return cure.CureExpressionCode switch
            {
                CureExpressionCode.Basic => BasicEvaluate(actor, cure.Rate),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private int BasicEvaluate(CharacterEntity actor, float rate)
        {
            var wisdom = _characterPropertyFactory.Create(actor.Id).Wisdom;
            var restore = (int)(wisdom * 8 * rate) + _myRandom.Range(0, 2);
            var currentHitPoint = actor.CurrentHitPoint;
            var maxHitPoint = _characterPropertyFactory.Create(actor.Id).HitPoint;
            var actualRestore = Math.Min(restore, maxHitPoint - currentHitPoint);
            return actualRestore;
        }
    }
}