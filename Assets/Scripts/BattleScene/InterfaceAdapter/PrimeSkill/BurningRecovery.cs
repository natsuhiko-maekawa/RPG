using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;

namespace BattleScene.InterfaceAdapter.PrimeSkill
{
    public class BurningRecovery : BaseRecovery
    {
        public override IReadOnlyList<SlipCode> SlipCodeList { get; } = new[] { SlipCode.Burning };
    }
}