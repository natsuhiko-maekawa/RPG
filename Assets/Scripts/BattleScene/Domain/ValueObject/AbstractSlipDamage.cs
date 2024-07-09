using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.Interface;

namespace BattleScene.UseCases.Skill.SkillElement.AbstractClass
{
    public abstract class AbstractSlipDamage : ILuckSkillElement
    {
        public virtual float GetLuckRate()
        {
            return 0.5f;
        }

        public abstract SlipDamageCode GetSlipDamageCode();
    }
}