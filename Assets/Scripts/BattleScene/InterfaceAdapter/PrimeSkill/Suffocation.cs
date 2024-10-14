using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;

namespace BattleScene.InterfaceAdapter.PrimeSkill
{
    public class Suffocation : BaseSlip
    {
        public override SlipDamageCode SlipDamageCode { get; } = SlipDamageCode.Suffocation;
    }
}