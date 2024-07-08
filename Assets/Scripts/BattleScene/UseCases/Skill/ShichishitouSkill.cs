using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.AbstractClass;
using BattleScene.UseCases.Skill.SkillElement;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     七支刀
    /// </summary>
    internal class ShichishitouSkill : AbstractSkill
    {
        private readonly Shichishitou _shichishitou;

        public ShichishitouSkill(Shichishitou shichishitou)
        {
            DamageList = ImmutableList.Create<AbstractDamage>(shichishitou);
        }

        public override int GetTechnicalPoint()
        {
            return 5;
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
            return PlayerImageCode.Katana;
        }

        public override MessageCode GetDescription()
        {
            return MessageCode.ShichishitouDescription;
        }

        public override MessageCode GetAttackMessage()
        {
            return MessageCode.AttackMessage;
        }
    }
}