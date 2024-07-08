using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.SkillElement;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     ランダムショット
    /// </summary>
    internal class RandomShotsSkill : AbstractSkill
    {
        public RandomShotsSkill(RandomShot randomShot)
        {
            DamageSkillElementList = ImmutableList.Create<AbstractDamage>(randomShot);
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