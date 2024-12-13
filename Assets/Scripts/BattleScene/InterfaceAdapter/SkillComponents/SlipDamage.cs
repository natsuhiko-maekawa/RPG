using BattleScene.Domain.Codes;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.SkillComponents
{
    public class SlipDamage : BaseDamage
    {
        public override DamageExpressionCode DamageExpressionCode { get; } = DamageExpressionCode.Slip;
        public override HitEvaluationCode HitEvaluationCode { get; } = HitEvaluationCode.AlwaysHit;
    }
}