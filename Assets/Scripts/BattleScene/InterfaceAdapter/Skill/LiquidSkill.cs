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
    ///     粘液
    /// </summary>
    public class LiquidSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Liquid;
        public override Range Range { get; } = Range.Solo;
        public override MessageCode AttackMessageCode { get; } = MessageCode.InvasiveMouthMessage;

        public override IReadOnlyList<BaseDamage> DamageList { get; }
            = new [] { new BasicDamage() };

        public override IReadOnlyList<BaseSlip> SlipDamageList { get; }
            = new [] { new Suffocation() };
    }
}