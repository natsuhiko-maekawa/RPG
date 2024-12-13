using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Presenters.SkillComponents.BaseClass;

namespace BattleScene.Presenters.SkillComponents
{
    public class BurningRecovery : BaseRecovery
    {
        public override IReadOnlyList<SlipCode> SlipCodeList { get; } = new[] { SlipCode.Burning };
    }
}