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
    ///     痺れる粘液
    /// </summary>
    public class NumbLiquidSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.NumbLiquid;
        public override Range Range { get; } = Range.Solo;
        public override MessageCode AttackMessageCode { get; } = MessageCode.NumbLiquidMessage;

        public override IReadOnlyList<BaseDamage> DamageList { get; }
            = new [] { new BasicDamage() };

        public override IReadOnlyList<BaseAilment> AilmentList { get; }
            = new [] { new Paralysis() };
    }
}