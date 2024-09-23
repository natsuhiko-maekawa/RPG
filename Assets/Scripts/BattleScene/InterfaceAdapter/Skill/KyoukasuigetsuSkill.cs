using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;
using BattleScene.InterfaceAdapter.Skill.PrimeSkill;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     鏡花水月
    /// </summary>
    internal class KyoukasuigetsuSkill : AbstractSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Kyoukasuigetsu;
        public override ImmutableList<BodyPartCode> DependencyList { get; } = ImmutableList.Create(BodyPartCode.Arm);
        public override Range Range { get; } = Range.Line;
        public override PlayerImageCode PlayerImageCode { get; } = PlayerImageCode.Katana;
        public override MessageCode Description { get; } = MessageCode.KyoukasuigetsuDescription;
        public override MessageCode AttackMessageCode { get; } = MessageCode.NoMessage;

        public override ImmutableList<AbstractAilment> AilmentList { get; } 
            = ImmutableList.Create<AbstractAilment>(new AbsoluteConfusion());

        public override ImmutableList<AbstractDamage> DamageList { get; }
            = ImmutableList.Create<AbstractDamage>(new BasicDamage());
    }
}