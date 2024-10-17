using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;
using BattleScene.InterfaceAdapter.Skill.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     雷切
    /// </summary>
    public class RaikiriSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Raikiri;
        public override int TechnicalPoint { get; } = 5;
        public override IReadOnlyList<BodyPartCode> DependencyList { get; } = new [] { BodyPartCode.Arm };
        public override Range Range { get; } = Range.Solo;
        public override MessageCode AttackMessageCode { get; } = MessageCode.AttackMessage;

        public override IReadOnlyList<BaseDamage> DamageList { get; }
            = new [] { new LightningDamage() };
    }
}