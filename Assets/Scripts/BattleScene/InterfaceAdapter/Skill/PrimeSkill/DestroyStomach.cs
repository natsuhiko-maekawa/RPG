using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;

namespace BattleScene.InterfaceAdapter.Skill.PrimeSkill
{
    public class DestroyStomach : AbstractDestroy
    {
        public override BodyPartCode BodyPartCode { get; } = BodyPartCode.Stomach;

        public override BodyPartCode GetDestroyPart()
        {
            return BodyPartCode.Stomach;
        }
    }
}