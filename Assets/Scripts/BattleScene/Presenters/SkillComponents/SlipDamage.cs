using BattleScene.Domain.Codes;
using BattleScene.Presenters.SkillComponents.BaseClass;

namespace BattleScene.Presenters.SkillComponents
{
    public class SlipDamage : BaseDamage
    {
        public override DamageExpressionCode DamageExpressionCode { get; } = DamageExpressionCode.Slip;
        public override HitEvaluationCode HitEvaluationCode { get; } = HitEvaluationCode.AlwaysHit;
    }
}