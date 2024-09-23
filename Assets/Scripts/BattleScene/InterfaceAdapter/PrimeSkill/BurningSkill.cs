using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;

namespace BattleScene.InterfaceAdapter.PrimeSkill
{
    public class BurningSkill : BaseSlip
    {
        public override SlipDamageCode SlipDamageCode { get; } = SlipDamageCode.Burning;
    }
}