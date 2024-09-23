using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;
using BattleScene.InterfaceAdapter.Skill.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     鬼殺し
    /// </summary>
    internal class OnikoroshiSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Onikoroshi;
        public override ImmutableList<BodyPartCode> DependencyList { get; } = ImmutableList.Create(BodyPartCode.Arm);
        public override Range Range { get; } = Range.Solo;
        public override MessageCode AttackMessageCode { get; } = MessageCode.OnikoroshiMessage;

        public override ImmutableList<BaseAilment> AilmentList { get; }
            = ImmutableList.Create<BaseAilment>(new Confusion());
    }
}