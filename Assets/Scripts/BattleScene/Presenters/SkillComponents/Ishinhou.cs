using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Presenters.SkillComponents.BaseClass;

namespace BattleScene.Presenters.SkillComponents
{
    public class Ishinhou : BaseRecovery
    {
        public override IReadOnlyList<AilmentCode> AilmentCodeList { get; } = new[] { AilmentCode.Deaf };

        public override IReadOnlyList<SlipCode> SlipCodeList { get; } =
            new[] { SlipCode.Suffocation, SlipCode.Bleeding };
    }
}