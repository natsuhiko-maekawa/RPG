using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Skill.SkillElement;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     鬼殺し
    /// </summary>
    internal class OnikoroshiSkill : AbstractSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Onikoroshi;
        public override ImmutableList<BodyPartCode> DependencyList { get; } = ImmutableList.Create(BodyPartCode.Arm);
        public override Range Range { get; } = Range.Solo;
        public override MessageCode AttackMessageCode { get; } = MessageCode.OnikoroshiMessage;

        public override ImmutableList<AbstractAilment> AilmentList { get; }
            = ImmutableList.Create<AbstractAilment>(new Confusion());
    }
}