using System;
using BattleScene.Domain.Codes;
using BattleScene.Domain.Entities;
using BattleScene.Domain.ValueObjects;
using Utility;

namespace BattleScene.UseCases.Services
{
    public class AttacksWeakPointEvaluatorService
    {
        private readonly CharacterPropertyFactoryService _characterPropertyFactory;

        public AttacksWeakPointEvaluatorService(
            CharacterPropertyFactoryService characterPropertyFactoryFactory)
        {
            _characterPropertyFactory = characterPropertyFactoryFactory;
        }

        public bool Evaluate(CharacterEntity _, CharacterEntity target, DamageValueObject damage)
        {
            return damage.AttacksWeakPointEvaluationCode switch
            {
                AttacksWeakPointEvaluationCode.Basic => BasicEvaluate(target, damage),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private bool BasicEvaluate(CharacterEntity target, DamageValueObject damage)
        {
            var matchedWeakPointCode = _characterPropertyFactory.Create(target.Id).WeakPointsCode & damage.MatAttrCode;
            var matchedWeakPointCount = BitUtility.BitCount((uint)matchedWeakPointCode);
            var value = matchedWeakPointCount > 0;
            return value;
        }
    }
}