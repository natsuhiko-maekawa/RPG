using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCases.Skill.SkillElement;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     雷切
    /// </summary>
    internal class RaikiriSkill : AbstractSkill
    {
        private readonly LightningDamageSkillElement _lightningDamageSkillElement;

        public RaikiriSkill(LightningDamageSkillElement lightningDamageSkillElement)
        {
            _lightningDamageSkillElement = lightningDamageSkillElement;
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
            return MessageCode.RaikiriDescription;
        }

        public override MessageCode GetAttackMessage()
        {
            return MessageCode.DamageMessage;
        }

        public override ImmutableList<ISkillElement> GetSkillService()
        {
            return ImmutableList.Create<ISkillElement>(_lightningDamageSkillElement);
        }
    }
}