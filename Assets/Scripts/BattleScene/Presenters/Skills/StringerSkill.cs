using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Presenters.SkillComponents;
using BattleScene.Presenters.SkillComponents.BaseClass;
using BattleScene.Presenters.Skills.BaseClass;

namespace BattleScene.Presenters.Skills
{
    /// <summary>
    ///     毒針
    /// </summary>
    public class StringerSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Stringer;
        public override Range Range { get; } = Range.Solo;
        public override MessageCode AttackMessageCode { get; } = MessageCode.StringerMessage;

        public override IReadOnlyList<BaseDamage> DamageList { get; }
            = new[] { new BasicDamage() };

        public override IReadOnlyList<BaseSlip> SlipList { get; }
            = new[] { new Poisoning() };
    }
}