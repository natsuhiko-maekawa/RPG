using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCases.Skill.SkillElement;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     村雨
    /// </summary>
    internal class MurasameSkill : AbstractSkill
    {
        private readonly BasicDamageSkillElement _basicDamageSkillElement;
        private readonly BurningResetSkillElement _burningResetSkillElement;

        public MurasameSkill(
            BasicDamageSkillElement basicDamageSkillElement,
            BurningResetSkillElement burningResetSkillElement)
        {
            _basicDamageSkillElement = basicDamageSkillElement;
            _burningResetSkillElement = burningResetSkillElement;
        }

        public override int GetTechnicalPoint()
        {
            return 5;
        }

        public override Range GetRange()
        {
            return Range.Solo;
        }

        public override PlayerImageCode GetPlayerImageCode()
        {
            return PlayerImageCode.Katana;
        }

        public override MessageCode GetDescription()
        {
            return MessageCode.MurasameDescription;
        }

        public override MessageCode GetAttackMessage()
        {
            return MessageCode.DamageMessage;
        }

        public override ImmutableList<ISkillElement> GetSkillService()
        {
            return ImmutableList.Create<ISkillElement>(_basicDamageSkillElement, _burningResetSkillElement);
        }
    }
}