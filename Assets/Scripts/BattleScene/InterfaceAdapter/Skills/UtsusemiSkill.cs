using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.InterfaceAdapter.SkillComponents;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;
using BattleScene.InterfaceAdapter.Skills.BaseClass;
using Range = BattleScene.Domain.Codes.Range;

namespace BattleScene.InterfaceAdapter.Skills
{
    /// <summary>
    ///     空蝉
    /// </summary>
    public class UtsusemiSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Utsusemi;
        public override int TechnicalPoint { get; } = 5;
        public override Range Range { get; } = Range.Oneself;
        public override MessageCode AttackMessageCode { get; } = MessageCode.SkillMessage;

        public override IReadOnlyList<BaseEnhance> EnhanceList { get; } = new[] { new Utsusemi() };
    }
}