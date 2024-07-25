using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;

namespace BattleScene.InterfaceAdapter.Skill.SkillElement
{
    public class destroyLeg : AbstractDestroyPart
    {
        public override BodyPartCode GetDestroyPart()
        {
            return BodyPartCode.Leg;
        }
    }
}