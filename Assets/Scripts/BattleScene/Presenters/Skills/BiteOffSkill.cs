﻿using System;
using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Presenters.SkillComponents;
using BattleScene.Presenters.SkillComponents.BaseClass;
using BattleScene.Presenters.Skills.BaseClass;
using BattleScene.UseCases.IServices;
using Range = BattleScene.Domain.Codes.Range;

namespace BattleScene.Presenters.Skills
{
    /// <summary>
    ///     食いちぎり
    /// </summary>
    public class BiteOffSkill : BaseSkill
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

        public override IReadOnlyList<BaseDamage> DamageList { get; } =
            new[] { new BasicDamage() };

        public override IReadOnlyList<BaseSlip> SlipList { get; } =
            new[] { new Bleeding() };

        public override IReadOnlyList<BaseDestroy> DestroyList => GetDestroyPartList();

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

        private IReadOnlyList<BaseDestroy> GetDestroyPartList()
        {
            var destroyList = new List<BaseDestroy>()
                { new DestroyArm(), new DestroyLeg(), new DestroyStomach() };
            return new[] { _myRandom.Choice(destroyList, _seed) };
        }
    }
}