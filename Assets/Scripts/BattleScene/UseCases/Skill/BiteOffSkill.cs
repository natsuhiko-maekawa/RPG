using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Skill.SkillElement;
using Utility.Interface;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     食いちぎり
    /// </summary>
    internal class BiteOffSkill : AbstractSkill
    {
        private readonly IRandomEx _randomEx;
        private long _seed;

        public BiteOffSkill(
            BasicDamage basicDamage,
            BleedingSkill bleedingSkill,
            destroyArm destroyArm,
            destroyLeg destroyLeg,
            destroyStomach destroyStomach,
            IRandomEx randomEx)
        {
            _randomEx = randomEx;
            DamageList = ImmutableList.Create<AbstractDamage>(basicDamage);
            SlipDamageList = ImmutableList.Create<AbstractSlipDamage>(bleedingSkill);
            SetDestroyPart(destroyArm, destroyLeg, destroyStomach);
        }

        public override Range GetRange()
        {
            return Range.Solo;
        }

        public override MessageCode GetAttackMessage()
        {
            var attackMessageList = new List<MessageCode>
            {
                MessageCode.BiteArmMessage,
                MessageCode.BiteLegMessage,
                MessageCode.BiteStomachMessage
            };

            return _randomEx.Choice(attackMessageList, _seed);
        }

        private void SetDestroyPart(params AbstractDestroyPart[] destroyPartSkillElementList)
        {
            _seed = DateTime.Now.Ticks;
            var destroyPartSkillElement = _randomEx.Choice(destroyPartSkillElementList, _seed);
            DestroyPartList = ImmutableList.Create(destroyPartSkillElement);
        }
    }
}