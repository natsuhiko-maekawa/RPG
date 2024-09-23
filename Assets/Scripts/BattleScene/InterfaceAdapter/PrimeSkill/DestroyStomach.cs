using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;

namespace BattleScene.InterfaceAdapter.PrimeSkill
{
    public class DestroyStomach : BaseDestroy
    {
        public override BodyPartCode BodyPartCode { get; } = BodyPartCode.Stomach;
    }
}