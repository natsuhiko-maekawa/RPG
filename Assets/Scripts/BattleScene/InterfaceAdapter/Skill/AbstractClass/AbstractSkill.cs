using System.Collections.Immutable;
using BattleScene.Domain.Code;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.InterfaceAdapter.Skill.AbstractClass
{
    public abstract class AbstractSkill
    {
        public abstract SkillCode SkillCode { get; }
        public virtual int TechnicalPoint { get; } = 0;
        public virtual ImmutableList<BodyPartCode> DependencyList { get; } = ImmutableList<BodyPartCode>.Empty;
        public abstract Range Range { get; }
        public virtual PlayerImageCode PlayerImageCode { get; } = PlayerImageCode.NoImage;
        public virtual MessageCode Description { get; } = MessageCode.NoMessage;
        public abstract MessageCode AttackMessageCode { get; }

        public virtual ImmutableList<AbstractAilment> AilmentList { get; } = ImmutableList<AbstractAilment>.Empty;

        public virtual ImmutableList<AbstractSlipDamage> SlipDamageList { get; } =
            ImmutableList<AbstractSlipDamage>.Empty;

        public virtual ImmutableList<AbstractDestroyPart> DestroyList { get; } =
            ImmutableList<AbstractDestroyPart>.Empty;

        public virtual ImmutableList<AbstractDamage> DamageList { get; } = ImmutableList<AbstractDamage>.Empty;
        public virtual ImmutableList<AbstractBuff> BuffList { get; } = ImmutableList<AbstractBuff>.Empty;
        public virtual ImmutableList<AbstractCure> CureList { get; } = ImmutableList<AbstractCure>.Empty;
        public virtual ImmutableList<AbstractReset> ResetList { get; } = ImmutableList<AbstractReset>.Empty;
        public virtual ImmutableList<AbstractRestore> RestoreList { get; } = ImmutableList<AbstractRestore>.Empty;
    }
}