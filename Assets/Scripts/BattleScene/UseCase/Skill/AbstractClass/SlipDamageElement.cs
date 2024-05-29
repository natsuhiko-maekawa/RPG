using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCase.Skill.Interface;

namespace BattleScene.UseCase.Skill.AbstractClass
{
    public abstract class SlipDamageElement : ISkillElement, ILuckSkillElement
    {
        public virtual float GetLuckRate()
        {
            return 0.5f;
        }
        
        public abstract SlipDamageCode GetSlipDamageCode();
    }
}