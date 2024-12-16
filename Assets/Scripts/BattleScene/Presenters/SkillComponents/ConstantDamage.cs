using BattleScene.Domain.Codes;
using BattleScene.Presenters.SkillComponents.BaseClass;

namespace BattleScene.Presenters.SkillComponents
{
    public class ConstantDamage : BaseDamage
    {
        public override float DamageRate { get; } = 1.0f / 7.0f;
        public override DamageExpressionCode DamageExpressionCode { get; } = DamageExpressionCode.Constant;
    }
}