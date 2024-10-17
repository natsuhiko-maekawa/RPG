using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.InterfaceAdapter.Skill.BaseClass
{
    public abstract class BaseSkill
    {
        public abstract SkillCode SkillCode { get; }
        public virtual int TechnicalPoint { get; } = 0;
        public virtual IReadOnlyList<BodyPartCode> DependencyList { get; } = ImmutableList<BodyPartCode>.Empty;
        public abstract Range Range { get; }
        public abstract MessageCode AttackMessageCode { get; }

        public virtual IReadOnlyList<BaseAilment> AilmentList { get; } = ImmutableList<BaseAilment>.Empty;

        public virtual IReadOnlyList<BaseSlip> SlipDamageList { get; } =
            ImmutableList<BaseSlip>.Empty;

        public virtual IReadOnlyList<BaseDestroy> DestroyList { get; } =
            ImmutableList<BaseDestroy>.Empty;

        public virtual IReadOnlyList<BaseDamage> DamageList { get; } = ImmutableList<BaseDamage>.Empty;
        public virtual IReadOnlyList<BaseBuff> BuffList { get; } = ImmutableList<BaseBuff>.Empty;
        public virtual IReadOnlyList<BaseCure> CureList { get; } = ImmutableList<BaseCure>.Empty;
        public virtual IReadOnlyList<BaseReset> ResetList { get; } = ImmutableList<BaseReset>.Empty;
        public virtual IReadOnlyList<BaseRestore> RestoreList { get; } = ImmutableList<BaseRestore>.Empty;
    }
}