﻿using System.Collections.Generic;
using System.Collections.Generic;
using Utility;
using System.Collections.Immutable;
using BattleScene.Domain.Code;

namespace BattleScene.InterfaceAdapter.PrimeSkill.BaseClass
{
    public abstract class BaseDamage
    {
        public virtual int AttackNumber { get; } = 1;
        public virtual float DamageRate { get; } = 1.0f;
        public virtual IReadOnlyList<MatAttrCode> MatAttrCode { get; } = MyList<MatAttrCode>.Empty;
        public virtual DamageExpressionCode DamageExpressionCode { get; } = DamageExpressionCode.Basic;
        public virtual float HitRate { get; } = 1.0f;
        public virtual HitEvaluationCode HitEvaluationCode { get; } = HitEvaluationCode.Basic;

        public virtual AttacksWeakPointEvaluationCode AttacksWeakPointEvaluationCode { get; } =
            AttacksWeakPointEvaluationCode.Basic;
    }
}