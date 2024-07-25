using System;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.OldId;

namespace BattleScene.Domain.ValueObject
{
    public abstract class AbstractDamage
    {
        public virtual int AttackNumber { get; } = 1;
        public virtual float DamageRate { get; } = 1.0f;
        public virtual ImmutableList<MatAttrCode> MatAttrCode { get; } = ImmutableList<MatAttrCode>.Empty;
        public virtual DamageExpressionCode DamageExpressionCode { get; } = DamageExpressionCode.Basic;
        public virtual float HitRate { get; } = 1.0f;
        public virtual HitEvaluationCode HitEvaluationCode { get; } = HitEvaluationCode.Basic;

        public virtual AttacksWeakPointEvaluationCode AttacksWeakPointEvaluationCode { get; } =
            AttacksWeakPointEvaluationCode.Basic;

        [Obsolete]
        public virtual int GetAttackNum()
        {
            return 1;
        }

        [Obsolete]
        public virtual float GetDamageRate()
        {
            return 1.0f;
        }

        [Obsolete]
        public virtual ImmutableList<MatAttrCode> GetMatAttrCode()
        {
            return ImmutableList<MatAttrCode>.Empty;
        }

        [Obsolete]
        public virtual int GetDamageAmount(CharacterId targetId)
        {
            // var actorId = _orderedItems.FirstCharacterId();
            // return _damageExpression.Evaluate(actorId, targetId, this);
            throw new NotImplementedException();
        }

        [Obsolete]
        public virtual float GetHitRate()
        {
            return 1.0f;
        }

        [Obsolete]
        public virtual bool IsHit(CharacterId targetId)
        {
            // var actorId = _orderedItems.FirstCharacterId();
            // return _hitEvaluation.Evaluate(actorId, targetId, this);
            throw new NotImplementedException();

        }

        [Obsolete]
        public bool AttacksWeakPoint(CharacterId targetId)
        {
            // return _attacksWeakPointEvaluation.Evaluate(targetId, this);
            throw new NotImplementedException();
        }
    }
}