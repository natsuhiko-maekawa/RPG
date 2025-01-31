﻿using BattleScene.Domain.Codes;

namespace BattleScene.Presenters.SkillComponents.BaseClass
{
    public abstract class BaseDamage
    {
        public virtual int AttackCount { get; } = 1;
        public virtual float DamageRate { get; } = 1.0f;
        public virtual MatAttrCode MatAttrCode { get; } = MatAttrCode.NoAttr;
        public virtual DamageExpressionCode DamageExpressionCode { get; } = DamageExpressionCode.Basic;
        public virtual float HitRate { get; } = 1.0f;
        public virtual HitEvaluationCode HitEvaluationCode { get; } = HitEvaluationCode.Basic;

        public virtual AttacksWeakPointEvaluationCode AttacksWeakPointEvaluationCode { get; } =
            AttacksWeakPointEvaluationCode.Basic;
    }
}