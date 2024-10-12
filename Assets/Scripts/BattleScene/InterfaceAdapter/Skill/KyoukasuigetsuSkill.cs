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
    internal class KyoukasuigetsuSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Kyoukasuigetsu;
        public override ImmutableList<BodyPartCode> DependencyList { get; } = ImmutableList.Create(BodyPartCode.Arm);
        public override Range Range { get; } = Range.Line;
        public override MessageCode AttackMessageCode { get; } = MessageCode.NoMessage;

        public override ImmutableList<BaseAilment> AilmentList { get; } 
            = ImmutableList.Create<BaseAilment>(new AbsoluteConfusion());

        public override ImmutableList<BaseDamage> DamageList { get; }
            = ImmutableList.Create<BaseDamage>(new BasicDamage());
    }
}