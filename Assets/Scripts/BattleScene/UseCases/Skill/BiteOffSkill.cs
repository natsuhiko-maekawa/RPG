using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.SkillElement;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;
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
            DamageSkillElementList = ImmutableList.Create<AbstractDamage>(basicDamage);
            SlipDamageElementList = ImmutableList.Create<AbstractSlipDamage>(bleedingSkill);
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
            DestroyPartSkillElementList = ImmutableList.Create(destroyPartSkillElement);
        }
    }
}