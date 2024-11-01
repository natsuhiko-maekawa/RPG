using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;

namespace BattleScene.InterfaceAdapter.PrimeSkill
{
    public class FirstAid : BaseRecovery
    {
        public override IReadOnlyList<BodyPartCode> BodyPartCodeList { get; } =
            new[] { BodyPartCode.Arm, BodyPartCode.Leg, BodyPartCode.Stomach };
    }
}