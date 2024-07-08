using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.SkillElement;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     スモークボム
    /// </summary>
    internal class SmokeBombSkill : AbstractSkill
    {
        public SmokeBombSkill(BlindSkillElement blindSkillElement)
        {
            AilmentSkillElementList = ImmutableList.Create<AilmentSkillElement>(blindSkillElement);
        }

        public override int GetTechnicalPoint()
        {
            return 5;
        }

        public override Range GetRange()
        {
            return Range.Line;
        }

        public override ImmutableList<BodyPartCode> GetDependencyList()
        {
            return ImmutableList.Create(BodyPartCode.Arm);
        }

        public override PlayerImageCode GetPlayerImageCode()
        {
            return PlayerImageCode.Gun;
        }

        public override MessageCode GetDescription()
        {
            return MessageCode.SmokeBombDescription;
        }

        public override MessageCode GetAttackMessage()
        {
            return MessageCode.DamageMessage;
        }
    }
}