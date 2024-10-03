using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;
using BattleScene.InterfaceAdapter.Skill.BaseClass;
using BattleScene.UseCases.IService;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     食いちぎり
    /// </summary>
    internal class BiteOffSkill : BaseSkill
    {
        private readonly IMyRandomService _myRandom;
        private readonly long _seed;

        public BiteOffSkill(
            IMyRandomService myRandom)
        {
            _myRandom = myRandom;
            _seed = DateTime.Now.Ticks;
        }

        public override SkillCode SkillCode { get; } = SkillCode.BiteOff;
        public override Range Range { get; } = Range.Solo;
        public override MessageCode AttackMessageCode => GetAttackMessageCode();

        public override ImmutableList<BaseDamage> DamageList { get; } =
            ImmutableList.Create<BaseDamage>(new BasicDamage());

        public override ImmutableList<BaseSlip> SlipDamageList { get; } =
            ImmutableList.Create<BaseSlip>(new BleedingSkill());

        public override ImmutableList<BaseDestroy> DestroyList => GetDestroyPartList();

        private MessageCode GetAttackMessageCode()
        {
            var attackMessageList = new List<MessageCode>
            {
                MessageCode.BiteArmMessage,
                MessageCode.BiteLegMessage,
                MessageCode.BiteStomachMessage
            };

            return _myRandom.Choice(attackMessageList, _seed);
        }

        private ImmutableList<BaseDestroy> GetDestroyPartList()
        {
            var destroyList = new List<BaseDestroy>()
                { new DestroyArm(), new DestroyLeg(), new DestroyStomach() };
            return ImmutableList.Create(_myRandom.Choice(destroyList, _seed));
        }
    }
}