using System;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.AbstractClass;
using BattleScene.UseCases.Skill.SkillElement;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;
using static BattleScene.Domain.Code.PlayerImageCode;
using static BattleScene.Domain.Code.MessageCode;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     鏡花水月
    /// </summary>
    internal class KyoukasuigetsuSkill : AbstractSkill
    {
        public KyoukasuigetsuSkill(
            AbsoluteConfusion absoluteConfusion,
            BasicDamage basicDamage)
        {
            AilmentSkillElementList = ImmutableList.Create<AbstractAilment>(absoluteConfusion);
            DamageSkillElementList = ImmutableList.Create<AbstractDamage>(basicDamage);
        }

        public override ImmutableList<BodyPartCode> GetDependencyList()
        {
            return ImmutableList.Create(BodyPartCode.Arm);
        }

        public override Range GetRange()
        {
            return Range.Line;
        }

        public override PlayerImageCode GetPlayerImageCode()
        {
            return Katana;
        }

        public override MessageCode GetDescription()
        {
            return KyoukasuigetsuDescription;
        }

        public override MessageCode GetAttackMessage()
        {
            throw new NotImplementedException();
        }
    }
}