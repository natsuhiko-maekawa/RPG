﻿using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.Interface;
using BattleScene.UseCase.Skill.Expression;

namespace BattleScene.UseCase.Skill.AbstractClass
{
    public abstract class DamageSkillElement : ISkillElement
    {
        private readonly AttacksWeakPointEvaluation _attacksWeakPointEvaluation;
        private readonly DamageExpression _damageExpression;
        private readonly HitEvaluation _hitEvaluation;
        private readonly OrderedItemsDomainService _orderedItems;

        protected DamageSkillElement(
            AttacksWeakPointEvaluation attacksWeakPointEvaluation,
            DamageExpression damageExpression,
            HitEvaluation hitEvaluation,
            OrderedItemsDomainService orderedItems)
        {
            _attacksWeakPointEvaluation = attacksWeakPointEvaluation;
            _damageExpression = damageExpression;
            _hitEvaluation = hitEvaluation;
            _orderedItems = orderedItems;
        }

        public virtual int GetAttackNum()
        {
            return 1;
        }

        public virtual float GetDamageRate()
        {
            return 1.0f;
        }

        public virtual ImmutableList<MatAttrCode> GetMatAttrCode()
        {
            return ImmutableList<MatAttrCode>.Empty;
        }

        public virtual int GetDamageAmount(CharacterId targetId)
        {
            var actorId = _orderedItems.FirstCharacterId();
            return _damageExpression.Evaluate(actorId, targetId, this);
        }

        public virtual float GetHitRate()
        {
            return 1.0f;
        }

        public virtual bool IsHit(CharacterId targetId)
        {
            var actorId = _orderedItems.FirstCharacterId();
            return _hitEvaluation.Evaluate(actorId, targetId, this);
        }

        public bool AttacksWeakPoint(CharacterId targetId)
        {
            return _attacksWeakPointEvaluation.Evaluate(targetId, this);
        }
    }
}