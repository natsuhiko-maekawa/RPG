using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;

namespace BattleScene.InterfaceAdapter.Skill.SkillElement
{
    public class DestroyStomach : AbstractDestroyPart
    {
        public override BodyPartCode BodyPartCode { get; } = BodyPartCode.Stomach;

        public override BodyPartCode GetDestroyPart()
        {
            return BodyPartCode.Stomach;
        }
    }
}