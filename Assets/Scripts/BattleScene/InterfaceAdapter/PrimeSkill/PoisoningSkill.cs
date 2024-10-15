using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;

namespace BattleScene.InterfaceAdapter.PrimeSkill
{
    public class PoisoningSkill : BaseSlip
    {
        public override SlipCode SlipCode { get; } = SlipCode.Poisoning;
    }
}