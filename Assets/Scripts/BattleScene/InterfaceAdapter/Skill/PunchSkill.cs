using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Skill.SkillElement;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     殴りつけ
    /// </summary>
    internal class PunchSkill : AbstractSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Punch;
        public override ImmutableList<BodyPartCode> DependencyList { get; } = ImmutableList.Create(BodyPartCode.Arm);
        public override Range Range { get; } = Range.Solo;
        public override MessageCode AttackMessageCode { get; } = MessageCode.PunchStomachMessage;

        public override ImmutableList<AbstractDamage> DamageList { get; }
            = ImmutableList.Create<AbstractDamage>(new BasicDamage());
    }
}