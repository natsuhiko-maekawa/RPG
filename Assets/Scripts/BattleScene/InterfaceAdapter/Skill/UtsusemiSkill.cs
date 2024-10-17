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
    ///     空蝉
    /// </summary>
    public class UtsusemiSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Utsusemi;
        public override int TechnicalPoint { get; } = 5;
        public override Range Range { get; } = Range.Oneself;
        public override MessageCode AttackMessageCode { get; } = MessageCode.NoMessage;

        public override IReadOnlyList<BaseBuff> BuffList { get; }
            = new [] { new Utsusemi() };
    }
}