using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.SkillComponents
{
    public class BurningRecovery : BaseRecovery
    {
        public override IReadOnlyList<SlipCode> SlipCodeList { get; } = new[] { SlipCode.Burning };
    }
}