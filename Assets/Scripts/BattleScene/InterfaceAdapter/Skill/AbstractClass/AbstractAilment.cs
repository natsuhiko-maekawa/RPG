using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;

namespace BattleScene.InterfaceAdapter.Skill.AbstractClass
{
    public abstract class AbstractAilment : ILuckSkillElement
    {
        public virtual float GetLuckRate()
        {
            return 0.5f;
        }

        public abstract AilmentCode GetAilmentCode();
    }
}