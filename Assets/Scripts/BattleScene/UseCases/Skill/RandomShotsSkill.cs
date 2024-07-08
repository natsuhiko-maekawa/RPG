using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCases.Skill.SkillElement;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     ランダムショット
    /// </summary>
    internal class RandomShotsSkill : AbstractSkill
    {
        public RandomShotsSkill(RandomShotSkillElement randomShotSkillElement)
        {
            DamageSkillElementList = ImmutableList.Create<DamageSkillElement>(randomShotSkillElement);
        }

        public override int GetTechnicalPoint()
        {
            return 15;
        }

        public override Range GetRange()
        {
            return Range.Random;
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
            return MessageCode.RandomShotDescription;
        }

        public override MessageCode GetAttackMessage()
        {
            return MessageCode.DamageMessage;
        }
    }
}