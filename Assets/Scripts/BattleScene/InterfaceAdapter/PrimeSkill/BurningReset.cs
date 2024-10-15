using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;

namespace BattleScene.InterfaceAdapter.PrimeSkill
{
    public class BurningReset : BaseReset
    {
        public override IReadOnlyList<SlipCode> SlipDamageCodeList { get; } = new[] { SlipCode.Burning };
    }
}