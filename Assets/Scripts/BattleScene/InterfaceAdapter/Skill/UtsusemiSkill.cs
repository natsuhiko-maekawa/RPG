using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Skill.SkillElement;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     空蝉
    /// </summary>
    internal class UtsusemiSkill : AbstractSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Utsusemi;
        public override int TechnicalPoint { get; } = 5;
        public override Range Range { get; } = Range.Oneself;
        public override PlayerImageCode PlayerImageCode { get; } = PlayerImageCode.Katana;
        public override MessageCode Description { get; } = MessageCode.UtsusemiDescription;
        public override MessageCode AttackMessageCode { get; } = MessageCode.NoMessage;

        public override ImmutableList<AbstractBuff> BuffList { get; }
            = ImmutableList.Create<AbstractBuff>(new Utsusemi());
    }
}