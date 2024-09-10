using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using Utility.Interface;

namespace BattleScene.UseCases.Service
{
    public class CureEvaluatorService
    {
        private readonly CharacterPropertyFactoryService _characterPropertyFactory;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly IRandomEx _randomEx;
        
        public int Evaluate(CharacterId actorId, CureParameterValueObject cureParameter)
        {
            return cureParameter.CureExpressionCode switch
            {
                CureExpressionCode.Basic => BasicEvaluate(actorId),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        private int BasicEvaluate(CharacterId actorId)
        {
            var wisdom = _characterPropertyFactory.Crate(actorId).Wisdom;
            var restore = wisdom * 8 + _randomEx.Range(0, 2);
            var currentHitPoint = _characterRepository.Select(actorId).CurrentHitPoint;
            var maxHitPoint = _characterPropertyFactory.Crate(actorId).HitPoint;
            var actualRestore = Math.Min(restore, maxHitPoint - currentHitPoint);
            return actualRestore;
        }
    }
}