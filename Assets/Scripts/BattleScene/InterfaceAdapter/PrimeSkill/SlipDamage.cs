using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;

namespace BattleScene.InterfaceAdapter.PrimeSkill
{
    public class SlipDamage : BaseDamage
    {
        public override DamageExpressionCode DamageExpressionCode { get; } = DamageExpressionCode.Slip;
        public override HitEvaluationCode HitEvaluationCode { get; } = HitEvaluationCode.AlwaysHit;
    }
}