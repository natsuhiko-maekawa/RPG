using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;

namespace BattleScene.InterfaceAdapter.PrimeSkill
{
    public class LightningDamage : BaseDamage
    {
        public override MatAttrCode MatAttrCode { get; } = MatAttrCode.Lightning;
    }
}