﻿using BattleScene.Domain.Code;

namespace BattleScene.InterfaceAdapter.PrimeSkill.BaseClass
{
    public abstract class BaseCure
    {
        public virtual float Rate { get; } = 1.0f;
        public virtual CureExpressionCode CureExpressionCode { get; } = CureExpressionCode.Basic;
    }
}