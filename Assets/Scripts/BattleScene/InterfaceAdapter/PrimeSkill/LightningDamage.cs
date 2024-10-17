using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;

namespace BattleScene.InterfaceAdapter.PrimeSkill
{
    public class LightningDamage : BaseDamage
    {
        public override IReadOnlyList<MatAttrCode> MatAttrCode { get; } =
            new[] { Domain.Code.MatAttrCode.Lightning };
    }
}