using BattleScene.Domain.Codes;
using BattleScene.Presenters.SkillComponents.BaseClass;

namespace BattleScene.Presenters.SkillComponents
{
    public class AlwaysHitDamage : BaseDamage
    {
        public override HitEvaluationCode HitEvaluationCode { get; } = HitEvaluationCode.AlwaysHit;
    }
}