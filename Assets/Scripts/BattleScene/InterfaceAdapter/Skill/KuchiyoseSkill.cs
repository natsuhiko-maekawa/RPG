using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;
using BattleScene.InterfaceAdapter.Skill.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     口寄せ
    /// </summary>
    internal class KuchiyoseSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Kuchiyose;
        public override int TechnicalPoint { get; } = 10;
        public override Range Range { get; } = Range.Solo;
        public override MessageCode AttackMessageCode { get; } = MessageCode.NoMessage;

        public override ImmutableList<BaseAilment> AilmentList { get; }
            = ImmutableList.Create<BaseAilment>(new Confusion());
    }
}