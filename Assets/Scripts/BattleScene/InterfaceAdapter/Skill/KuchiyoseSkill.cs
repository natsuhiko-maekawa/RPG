using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.BaseClass;
using BattleScene.InterfaceAdapter.SkillComponents;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     口寄せ
    /// </summary>
    public class KuchiyoseSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Kuchiyose;
        public override int TechnicalPoint { get; } = 10;
        public override Range Range { get; } = Range.Solo;
        public override MessageCode AttackMessageCode { get; } = MessageCode.SkillMessage;

        public override IReadOnlyList<BaseAilment> AilmentList { get; }
            = new[] { new Confusion() };
    }
}