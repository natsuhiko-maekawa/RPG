using System;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;
using BattleScene.InterfaceAdapter.Skill.PrimeSkill;
using Utility.Interface;
using static BattleScene.Domain.Code.MessageCode;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     噛みつき
    /// </summary>
    internal class BiteSkill : AbstractSkill
    {
        private readonly IRandomEx _randomEx;
        private readonly long _seed;

        public BiteSkill(
            IRandomEx randomEx)
        {
            _randomEx = randomEx;
            _seed = DateTime.Now.Ticks;
        }

        public override SkillCode SkillCode { get; } = SkillCode.Bite;
        public override Range Range { get; } = Range.Solo;
        public override MessageCode AttackMessageCode => GetAttackMessageCode();

        public override ImmutableList<AbstractDamage> DamageList { get; }
            = ImmutableList.Create<AbstractDamage>(new BasicDamage());

        private MessageCode GetAttackMessageCode()
        {
            return _randomEx.Choice(new[] { BiteArmMessage, BiteLegMessage, BiteStomachMessage }, _seed);
        }
    }
}