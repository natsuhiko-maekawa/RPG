using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;

namespace BattleScene.InterfaceAdapter.PrimeSkill
{
    public class Burning : BaseSlip
    {
        public override SlipCode SlipCode { get; } = SlipCode.Burning;
    }
}