using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.SkillComponents
{
    public class FirstAid : BaseRecovery
    {
        public override IReadOnlyList<BodyPartCode> BodyPartCodeList { get; } =
            new[] { BodyPartCode.Arm, BodyPartCode.Leg, BodyPartCode.Stomach };
    }
}