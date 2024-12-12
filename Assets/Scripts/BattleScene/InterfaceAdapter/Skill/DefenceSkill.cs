using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.BaseClass;
using BattleScene.InterfaceAdapter.SkillComponents;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     防御
    /// </summary>
    public class DefenceSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Defence;
        public override Range Range { get; } = Range.Oneself;
        public override MessageCode AttackMessageCode { get; } = MessageCode.DefenceActionMessage;

        public override IReadOnlyList<BaseEnhance> EnhanceList { get; } = new[] { new Defence() };
        public override IReadOnlyList<BaseRestore> RestoreList { get; } = new[] { new BasicRestore() };
    }
}