using System;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Skill.SkillElement;
using Utility.Interface;
using static BattleScene.Domain.Code.MessageCode;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     切り刻み
    /// </summary>
    internal class CutUpSkill : AbstractSkill
    {
        private readonly IRandomEx _randomEx;
        private readonly long _seed;

        public CutUpSkill(
            IRandomEx randomEx)
        {
            _randomEx = randomEx;
            _seed = DateTime.Now.Ticks;
        }

        public override SkillCode SkillCode { get; } = SkillCode.CutUp;
        public override Range Range { get; } = Range.Solo;
        public override ImmutableList<BodyPartCode> DependencyList { get; } = ImmutableList.Create(BodyPartCode.Arm);
        public override MessageCode AttackMessageCode => GetAttackMessageCode();

        public override ImmutableList<AbstractDamage> DamageList { get; }
            = ImmutableList.Create<AbstractDamage>(new FiveTimeDamage());

        private MessageCode GetAttackMessageCode()
        {
            return _randomEx.Choice(new[] { CutUpArmMessage, CutUpLegMessage, CutUpStomachMessage }, _seed);
        }
    }
}