using System.Collections.Generic;
using Utility;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.InterfaceAdapter.Skill.BaseClass
{
    public abstract class BaseSkill
    {
        public abstract SkillCode SkillCode { get; }
        public virtual int TechnicalPoint { get; } = 0;
        public virtual IReadOnlyList<BodyPartCode> DependencyList { get; } = MyList<BodyPartCode>.Empty;
        public abstract Range Range { get; }
        public abstract MessageCode AttackMessageCode { get; }

        public virtual IReadOnlyList<BaseAilment> AilmentList { get; } = MyList<BaseAilment>.Empty;
        public virtual IReadOnlyList<BaseBuff> BuffList { get; } = MyList<BaseBuff>.Empty;
        public virtual IReadOnlyList<BaseCure> CureList { get; } = MyList<BaseCure>.Empty;
        public virtual IReadOnlyList<BaseDamage> DamageList { get; } = MyList<BaseDamage>.Empty;
        public virtual IReadOnlyList<BaseDestroy> DestroyList { get; } = MyList<BaseDestroy>.Empty;
        public virtual IReadOnlyList<BaseEnhance> EnhanceList { get; } = MyList<BaseEnhance>.Empty;
        public virtual IReadOnlyList<BaseRecovery> RecoveryList { get; } = MyList<BaseRecovery>.Empty;
        public virtual IReadOnlyList<BaseRestore> RestoreList { get; } = MyList<BaseRestore>.Empty;
        public virtual IReadOnlyList<BaseSlip> SlipList { get; } = MyList<BaseSlip>.Empty;
    }
}