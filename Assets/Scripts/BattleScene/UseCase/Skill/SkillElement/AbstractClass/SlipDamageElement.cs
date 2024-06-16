using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCase.Skill.Interface;

namespace BattleScene.UseCase.Skill.SkillElement.AbstractClass
{
    public abstract class SlipDamageElement : BaseClass.SkillElement, ILuckSkillElement
    {
        public virtual float GetLuckRate()
        {
            return 0.5f;
        }

        public abstract SlipDamageCode GetSlipDamageCode();
    }
}