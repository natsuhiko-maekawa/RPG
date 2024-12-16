using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Presenters.SkillComponents.BaseClass;

namespace BattleScene.Presenters.SkillComponents
{
    public class FirstAid : BaseRecovery
    {
        public override IReadOnlyList<BodyPartCode> BodyPartCodeList { get; } =
            new[] { BodyPartCode.Arm, BodyPartCode.Leg, BodyPartCode.Stomach };
    }
}