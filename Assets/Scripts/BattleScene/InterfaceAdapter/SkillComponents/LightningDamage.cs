using BattleScene.Domain.Codes;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.SkillComponents
{
    public class LightningDamage : BaseDamage
    {
        public override MatAttrCode MatAttrCode { get; } = MatAttrCode.Lightning;
    }
}