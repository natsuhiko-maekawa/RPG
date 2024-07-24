using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Skill.SkillElement;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     医心方
    /// </summary>
    internal class IshinhouSkill : AbstractSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Ishinhou;
        public override int TechnicalPoint { get; } = 3;
        public override Range Range { get; } = Range.Oneself;
        public override PlayerImageCode PlayerImageCode { get; } = PlayerImageCode.Katana;
        public override MessageCode Description { get; } = MessageCode.IshinhouDescription;
        public override MessageCode AttackMessageCode { get; } = MessageCode.RemoveAilmentsMessage;

        public override ImmutableList<AbstractReset> ResetList { get; }
            = ImmutableList.Create<AbstractReset>(new Ishinhou());
    }
}