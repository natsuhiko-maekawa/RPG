﻿using System;
using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Presenters.SkillComponents.BaseClass;
using Range = BattleScene.Domain.Codes.Range;

namespace BattleScene.Presenters.Skills.BaseClass
{
    public abstract class BaseSkill
    {
        public abstract SkillCode SkillCode { get; }
        public virtual int TechnicalPoint { get; } = 0;

        public virtual IReadOnlyList<BodyPartCode> DependencyList { get; } = Array.Empty<BodyPartCode>();

        // TODO: 攻撃スキルと回復スキルは対象が異なるため、RangeをSkillComponentに移動すること。
        public abstract Range Range { get; }
        public virtual bool IsAutoTarget { get; } = false;
        public abstract MessageCode AttackMessageCode { get; }

        public virtual IReadOnlyList<BaseAilment> AilmentList { get; } = Array.Empty<BaseAilment>();
        public virtual IReadOnlyList<BaseBuff> BuffList { get; } = Array.Empty<BaseBuff>();
        public virtual IReadOnlyList<BaseCure> CureList { get; } = Array.Empty<BaseCure>();
        public virtual IReadOnlyList<BaseDamage> DamageList { get; } = Array.Empty<BaseDamage>();
        public virtual IReadOnlyList<BaseDestroy> DestroyList { get; } = Array.Empty<BaseDestroy>();
        public virtual IReadOnlyList<BaseEnhance> EnhanceList { get; } = Array.Empty<BaseEnhance>();
        public virtual IReadOnlyList<BaseRecovery> RecoveryList { get; } = Array.Empty<BaseRecovery>();
        public virtual IReadOnlyList<BaseRestore> RestoreList { get; } = Array.Empty<BaseRestore>();
        public virtual IReadOnlyList<BaseSlip> SlipList { get; } = Array.Empty<BaseSlip>();
    }
}