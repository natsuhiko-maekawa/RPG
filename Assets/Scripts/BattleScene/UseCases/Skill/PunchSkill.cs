using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.SkillElement;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     殴りつけ
    /// </summary>
    internal class PunchSkill : AbstractSkill
    {
        private readonly BasicDamageSkillElement _basicDamageSkillElement;

        public PunchSkill(BasicDamageSkillElement basicDamageSkillElement)
        {
            DamageSkillElementList = ImmutableList.Create<DamageSkillElement>(basicDamageSkillElement);
        }

        public override Range GetRange()
        {
            return Range.Solo;
        }

        public override ImmutableList<BodyPartCode> GetDependencyList()
        {
            return ImmutableList.Create(BodyPartCode.Arm);
        }

        public override PlayerImageCode GetPlayerImageCode()
        {
            return PlayerImageCode.Damaged;
        }

        public override MessageCode GetAttackMessage()
        {
            return MessageCode.PunchStomachMessage;
        }
    }
}