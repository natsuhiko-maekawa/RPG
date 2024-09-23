using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;

namespace BattleScene.InterfaceAdapter.PrimeSkill
{
    public class DestroyLeg : BaseDestroy
    {
        public override BodyPartCode BodyPartCode { get; } = BodyPartCode.Leg;
        
        public override BodyPartCode GetDestroyPart()
        {
            return BodyPartCode.Leg;
        }
    }
}