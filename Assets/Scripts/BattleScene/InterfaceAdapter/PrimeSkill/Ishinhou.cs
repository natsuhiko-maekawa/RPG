using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;

namespace BattleScene.InterfaceAdapter.PrimeSkill
{
    public class Ishinhou : BaseRecovery
    {
        public override IReadOnlyList<AilmentCode> AilmentCodeList { get; } = new[] { AilmentCode.Deaf };

        public override IReadOnlyList<SlipCode> SlipCodeList { get; } =
            new[] { SlipCode.Suffocation, SlipCode.Bleeding };
    }
}