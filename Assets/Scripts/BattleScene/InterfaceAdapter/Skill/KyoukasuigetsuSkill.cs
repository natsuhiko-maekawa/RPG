using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;
using BattleScene.InterfaceAdapter.Skill.BaseClass;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     鏡花水月
    /// </summary>
    public class KyoukasuigetsuSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Kyoukasuigetsu;
        public override IReadOnlyList<BodyPartCode> DependencyList { get; } = new[] { BodyPartCode.Arm };
        public override Range Range { get; } = Range.Line;
        public override MessageCode AttackMessageCode { get; } = MessageCode.NoMessage;

        public override IReadOnlyList<BaseAilment> AilmentList { get; }
            = new[] { new AbsoluteConfusion() };

        public override IReadOnlyList<BaseDamage> DamageList { get; }
            = new[] { new BasicDamage() };
    }
}