using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;

namespace BattleScene.InterfaceAdapter.Skill.AbstractClass
{
    public abstract class AbstractDestroyPart
    {
        public float GetLuckRate()
        {
            return 0.5f;
        }

        public abstract BodyPartCode GetDestroyPart();
    }
}