using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCases.Skill.SkillElement;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     レーション
    /// </summary>
    internal class FieldRationSkill : AbstractSkill
    {
        public FieldRationSkill(BasicCureSkillElement basicCureSkillElement)
        {
            CureSkillElementList = ImmutableList.Create<CureSkillElement>(basicCureSkillElement);
        }

        public override int GetTechnicalPoint()
        {
            return 3;
        }

        public override PlayerImageCode GetPlayerImageCode()
        {
            return PlayerImageCode.Gun;
        }

        public override Range GetRange()
        {
            return Range.Oneself;
        }

        public override MessageCode GetDescription()
        {
            return MessageCode.FieldRationDescription;
        }

        public override MessageCode GetAttackMessage()
        {
            return MessageCode.RestoreHitPointMessage;
        }
    }
}