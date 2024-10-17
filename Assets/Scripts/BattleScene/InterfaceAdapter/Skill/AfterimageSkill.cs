using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;
using BattleScene.InterfaceAdapter.Skill.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     残像
    /// </summary>
    public class AfterimageSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Afterimage;
        public override Range Range { get; } = Range.Oneself;
        public override MessageCode AttackMessageCode { get; } = MessageCode.AfterimageMessage;

        public override IReadOnlyList<BaseBuff> BuffList { get; }
            = new [] { new AfterImage() };
    }
}