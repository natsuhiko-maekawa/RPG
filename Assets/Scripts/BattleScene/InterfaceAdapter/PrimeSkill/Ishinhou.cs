using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;

namespace BattleScene.InterfaceAdapter.PrimeSkill
{
    public class Ishinhou : BaseReset
    {
        public override IReadOnlyList<AilmentCode> AilmentCodeList { get; } = new[] { AilmentCode.Deaf };
        public override IReadOnlyList<SlipCode> SlipDamageCodeList { get; } =
            new[] { SlipCode.Suffocation, SlipCode.Bleeding };
    }
}