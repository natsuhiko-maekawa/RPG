using System;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Service
{
    public class AttacksWeakPointEvaluatorService
    {
        private readonly CharacterPropertyFactoryService _characterPropertyFactory;

        public AttacksWeakPointEvaluatorService(
            CharacterPropertyFactoryService characterPropertyFactoryFactory)
        {
            _characterPropertyFactory = characterPropertyFactoryFactory;
        }

        public bool Evaluate(CharacterId actorId, CharacterId targetId, DamageValueObject damage)
        {
            return damage.AttacksWeakPointEvaluationCode switch
            {
                AttacksWeakPointEvaluationCode.Basic => BasicEvaluate(targetId, damage),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private bool BasicEvaluate(CharacterId targetId, DamageValueObject damage)
        {
            return _characterPropertyFactory.Create(targetId).WeakPointsCodeList
                .Intersect(damage.MatAttrCode)
                .Any();
        }
    }
}