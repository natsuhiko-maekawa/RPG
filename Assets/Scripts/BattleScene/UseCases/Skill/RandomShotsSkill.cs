using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Skill.SkillElement;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     ランダムショット
    /// </summary>
    internal class RandomShotsSkill : AbstractSkill
    {
        public RandomShotsSkill(RandomShot randomShot)
        {
            DamageList = ImmutableList.Create<AbstractDamage>(randomShot);
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