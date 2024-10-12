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
        public virtual ImmutableList<BodyPartCode> DependencyList { get; } = ImmutableList<BodyPartCode>.Empty;
        public abstract Range Range { get; }
        public abstract MessageCode AttackMessageCode { get; }

        public virtual ImmutableList<BaseAilment> AilmentList { get; } = ImmutableList<BaseAilment>.Empty;

        public virtual ImmutableList<BaseSlip> SlipDamageList { get; } =
            ImmutableList<BaseSlip>.Empty;

        public virtual ImmutableList<BaseDestroy> DestroyList { get; } =
            ImmutableList<BaseDestroy>.Empty;

        public virtual ImmutableList<BaseDamage> DamageList { get; } = ImmutableList<BaseDamage>.Empty;
        public virtual ImmutableList<BaseBuff> BuffList { get; } = ImmutableList<BaseBuff>.Empty;
        public virtual ImmutableList<BaseCure> CureList { get; } = ImmutableList<BaseCure>.Empty;
        public virtual ImmutableList<BaseReset> ResetList { get; } = ImmutableList<BaseReset>.Empty;
        public virtual ImmutableList<BaseRestore> RestoreList { get; } = ImmutableList<BaseRestore>.Empty;
    }
}