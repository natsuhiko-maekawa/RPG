using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCases.Skill.SkillElement;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     レーション
    /// </summary>
    internal class FieldRationSkill : AbstractSkill
    {
        private readonly BasicCureSkillElement _basicCureSkillElement;

        public FieldRationSkill(BasicCureSkillElement basicCureSkillElement)
        {
            _basicCureSkillElement = basicCureSkillElement;
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

        public override ImmutableList<ISkillElement> GetSkillService()
        {
            return ImmutableList.Create<ISkillElement>(_basicCureSkillElement);
        }
    }
}