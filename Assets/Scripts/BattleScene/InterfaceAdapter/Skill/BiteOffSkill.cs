using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;
using BattleScene.InterfaceAdapter.Skill.PrimeSkill;
using Utility.Interface;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     食いちぎり
    /// </summary>
    internal class BiteOffSkill : AbstractSkill
    {
        private readonly IRandomEx _randomEx;
        private readonly long _seed;

        public BiteOffSkill(
            IRandomEx randomEx)
        {
            _randomEx = randomEx;
            _seed = DateTime.Now.Ticks;
        }

        public override SkillCode SkillCode { get; } = SkillCode.BiteOff;
        public override Range Range { get; } = Range.Solo;
        public override MessageCode AttackMessageCode => GetAttackMessageCode();

        public override ImmutableList<AbstractDamage> DamageList { get; } =
            ImmutableList.Create<AbstractDamage>(new BasicDamage());

        public override ImmutableList<AbstractSlipDamage> SlipDamageList { get; } =
            ImmutableList.Create<AbstractSlipDamage>(new BleedingSkill());

        public override ImmutableList<AbstractDestroy> DestroyList => GetDestroyPartList();

        private MessageCode GetAttackMessageCode()
        {
            var attackMessageList = new List<MessageCode>
            {
                MessageCode.BiteArmMessage,
                MessageCode.BiteLegMessage,
                MessageCode.BiteStomachMessage
            };

            return _randomEx.Choice(attackMessageList, _seed);
        }

        private ImmutableList<AbstractDestroy> GetDestroyPartList()
        {
            var destroyList = new List<AbstractDestroy>()
                { new DestroyArm(), new DestroyLeg(), new DestroyStomach() };
            return ImmutableList.Create(_randomEx.Choice(destroyList, _seed));
        }
    }
}