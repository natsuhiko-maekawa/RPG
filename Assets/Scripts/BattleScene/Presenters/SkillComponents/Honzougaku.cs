using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Presenters.SkillComponents.BaseClass;

namespace BattleScene.Presenters.SkillComponents
{
    public class Honzougaku : BaseRecovery
    {
        public override IReadOnlyList<AilmentCode> AilmentCodeList { get; } =
            new[] { AilmentCode.Blind, AilmentCode.Paralysis };

        public override IReadOnlyList<SlipCode> SlipCodeList { get; } = new[] { SlipCode.Poisoning };
    }
}