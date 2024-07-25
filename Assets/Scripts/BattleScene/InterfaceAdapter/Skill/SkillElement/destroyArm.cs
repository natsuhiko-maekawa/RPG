using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;

namespace BattleScene.InterfaceAdapter.Skill.SkillElement
{
    public class destroyArm : AbstractDestroyPart
    {
        public override BodyPartCode GetDestroyPart()
        {
            return BodyPartCode.Arm;
        }
    }
}