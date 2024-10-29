using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;

namespace BattleScene.InterfaceAdapter.PrimeSkill
{
    public class Honzougaku : BaseReset
    {
        public override IReadOnlyList<AilmentCode> AilmentCodeList { get; } =
            new[] { AilmentCode.Blind, AilmentCode.Paralysis };

        public override IReadOnlyList<SlipCode> SlipCodeList { get; } = new[] { SlipCode.Poisoning };
    }
}