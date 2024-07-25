using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;
using BattleScene.InterfaceAdapter.Skill.SkillElement;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     口寄せ
    /// </summary>
    internal class KuchiyoseSkill : AbstractSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Kuchiyose;
        public override int TechnicalPoint { get; } = 10;
        public override Range Range { get; } = Range.Solo;
        public override PlayerImageCode PlayerImageCode { get; } = PlayerImageCode.Katana;
        public override MessageCode Description { get; } = MessageCode.KuchiyoseDescription;
        public override MessageCode AttackMessageCode { get; } = MessageCode.NoMessage;

        public override ImmutableList<AbstractAilment> AilmentList { get; }
            = ImmutableList.Create<AbstractAilment>(new Confusion());
    }
}