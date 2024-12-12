using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.SkillComponents
{
    public class ConstantDamage : BaseDamage
    {
        public override float DamageRate { get; } = 1.0f / 7.0f;
        public override DamageExpressionCode DamageExpressionCode { get; } = DamageExpressionCode.Constant;
    }
}