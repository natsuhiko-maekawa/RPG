using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;

namespace BattleScene.InterfaceAdapter.PrimeSkill
{
    public class PoisoningSkill : BaseSlip
    {
        public override SlipDamageCode SlipDamageCode { get; } = SlipDamageCode.Poisoning;
    }
}