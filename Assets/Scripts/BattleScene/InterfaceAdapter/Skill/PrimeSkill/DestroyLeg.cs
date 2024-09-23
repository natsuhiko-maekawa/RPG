using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;

namespace BattleScene.InterfaceAdapter.Skill.PrimeSkill
{
    public class DestroyLeg : AbstractDestroy
    {
        public override BodyPartCode BodyPartCode { get; } = BodyPartCode.Leg;
        
        public override BodyPartCode GetDestroyPart()
        {
            return BodyPartCode.Leg;
        }
    }
}