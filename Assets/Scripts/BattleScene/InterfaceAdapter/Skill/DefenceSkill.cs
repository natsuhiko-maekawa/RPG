using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;
using BattleScene.InterfaceAdapter.Skill.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     防御
    /// </summary>
    public class DefenceSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Defence;
        public override Range Range { get; } = Range.Oneself;
        public override MessageCode AttackMessageCode { get; } = MessageCode.DefenceMessage;

        public override IReadOnlyList<BaseBuff> BuffList { get; }
            = new[] { new Defence() };

        public override IReadOnlyList<BaseRestore> RestoreList { get; }
            = new[] { new BasicRestore() };
    }
}