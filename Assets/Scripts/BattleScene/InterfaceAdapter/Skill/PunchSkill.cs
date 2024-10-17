using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;
using BattleScene.InterfaceAdapter.Skill.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     殴りつけ
    /// </summary>
    public class PunchSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Punch;
        public override IReadOnlyList<BodyPartCode> DependencyList { get; } = new[] { BodyPartCode.Arm };
        public override Range Range { get; } = Range.Solo;
        public override MessageCode AttackMessageCode { get; } = MessageCode.PunchStomachMessage;

        public override IReadOnlyList<BaseDamage> DamageList { get; }
            = new[] { new BasicDamage() };
    }
}