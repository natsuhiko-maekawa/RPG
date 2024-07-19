using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Skill.SkillElement;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     シルバーバレット
    /// </summary>
    internal class SilverBulletSkill : AbstractSkill
    {
        public SilverBulletSkill(ConstantDamage constantDamage)
        {
            DamageList = ImmutableList.Create<AbstractDamage>(constantDamage);
        }

        public override int GetTechnicalPoint()
        {
            return 7;
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
            return PlayerImageCode.Gun;
        }

        public override MessageCode GetDescription()
        {
            return MessageCode.SilverBulletDescription;
        }

        public override MessageCode GetAttackMessage()
        {
            return MessageCode.AttackMessage;
        }
    }
}