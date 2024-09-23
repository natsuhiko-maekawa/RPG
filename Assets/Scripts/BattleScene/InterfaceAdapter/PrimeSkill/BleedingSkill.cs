using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;

namespace BattleScene.InterfaceAdapter.PrimeSkill
{
    public class BleedingSkill : BaseSlip
    {
        public override SlipDamageCode SlipDamageCode { get; } = SlipDamageCode.Bleeding;
    }
}