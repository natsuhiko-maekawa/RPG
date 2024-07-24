using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Skill.SkillElement
{
    public class AlwaysHitDamage : AbstractDamage
    {
        public override HitEvaluationCode HitEvaluationCode { get; } = HitEvaluationCode.AlwaysHit;
    }
}