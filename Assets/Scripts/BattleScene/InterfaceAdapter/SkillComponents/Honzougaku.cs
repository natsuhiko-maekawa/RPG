using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.SkillComponents
{
    public class Honzougaku : BaseRecovery
    {
        public override IReadOnlyList<AilmentCode> AilmentCodeList { get; } =
            new[] { AilmentCode.Blind, AilmentCode.Paralysis };

        public override IReadOnlyList<SlipCode> SlipCodeList { get; } = new[] { SlipCode.Poisoning };
    }
}