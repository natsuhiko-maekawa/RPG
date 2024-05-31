using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCase.Skill.SkillElement;
using Utility;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.UseCase.Skill
{
    /// <summary>
    ///     食いちぎり
    /// </summary>
    internal class BiteOffSkill : AbstractSkill
    {
        private readonly BasicDamageSkillElement _basicDamageSkillElement;
        private readonly BleedingSkillElement _bleedingSkillElement;
        private readonly DestroyArmSkillElement _destroyArmSkillElement;
        private readonly DestroyLegSkillElement _destroyLegSkillElement;
        private readonly DestroyStomachSkillElement _destroyStomachSkillElement;
        private readonly IRandomEx _randomEx;
        private long _seed;

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

        public override ImmutableList<ISkillElement> GetSkillService()
        {
            _seed = DateTime.Now.Ticks;
            var skillElementList = new List<ISkillElement>
            {
                _basicDamageSkillElement,
                _bleedingSkillElement
            };

            var destroyPartSkillElementList
                = new List<ISkillElement>
                    { _destroyArmSkillElement, _destroyLegSkillElement, _destroyStomachSkillElement };
            var destroyPartSkillElement = _randomEx.Choice(destroyPartSkillElementList, _seed);
            skillElementList.Add(destroyPartSkillElement);
            return skillElementList.ToImmutableList();
        }
    }
}