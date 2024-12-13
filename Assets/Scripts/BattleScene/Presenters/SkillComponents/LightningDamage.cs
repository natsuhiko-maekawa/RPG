using BattleScene.Domain.Codes;
using BattleScene.Presenters.SkillComponents.BaseClass;

namespace BattleScene.Presenters.SkillComponents
{
    public class LightningDamage : BaseDamage
    {
        public override MatAttrCode MatAttrCode { get; } = MatAttrCode.Lightning;
    }
}