using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;

namespace BattleScene.InterfaceAdapter.PrimeSkill
{
    public class LightningDamage : BaseDamage
    {
        public override ImmutableList<MatAttrCode> MatAttrCode { get; } =
            ImmutableList.Create(Domain.Code.MatAttrCode.Lightning);
    }
}