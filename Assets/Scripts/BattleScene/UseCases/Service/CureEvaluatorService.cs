using System;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;
using BattleScene.Domain.ValueObject;
using Utility.Interface;

namespace BattleScene.UseCases.Service
{
    public class CureEvaluatorService
    {
        private readonly IRepository<CharacterAggregate, CharacterId> _characterRepository;
        private readonly IRepository<HitPointAggregate, CharacterId> _hitPointRepository;
        private readonly IRandomEx _randomEx;
        
        public int Evaluate(CharacterId actorId, AbstractCure cure)
        {
            return cure.CureExpressionCode switch
            {
                CureExpressionCode.Basic => BasicEvaluate(actorId),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        private int BasicEvaluate(CharacterId actorId)
        {
            var actor = _characterRepository.Select(actorId);
            var restore = actor.Property.Wisdom * 8 + _randomEx.Range(0, 2);
            return _hitPointRepository.Select(actorId).GetRestore(restore);
        }
    }
}