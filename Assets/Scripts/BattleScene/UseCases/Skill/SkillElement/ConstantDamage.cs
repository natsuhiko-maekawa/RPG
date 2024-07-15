﻿using BattleScene.Domain.DomainService;
using BattleScene.Domain.Expression;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Skill.SkillElement
{
    public class ConstantDamage : AbstractDamage
    {
        private readonly ConstantDamageExpression _constantDamageExpression;
        private readonly OrderedItemsDomainService _orderedItems;

        public ConstantDamage(
            AttacksWeakPointEvaluation attacksWeakPointEvaluation,
            DamageExpression damageExpression,
            HitEvaluation hitEvaluation,
            OrderedItemsDomainService orderedItems,
            ConstantDamageExpression constantDamageExpression)
            : base(
                attacksWeakPointEvaluation,
                damageExpression,
                hitEvaluation,
                orderedItems)
        {
            _constantDamageExpression = constantDamageExpression;
            _orderedItems = orderedItems;
        }

        public override float GetDamageRate()
        {
            return 1.0f / 7.0f;
        }

        public override int GetDamageAmount(CharacterId targetId)
        {
            var actorId = _orderedItems.FirstCharacterId();
            return _constantDamageExpression.Evaluate(actorId, this);
        }
    }
}