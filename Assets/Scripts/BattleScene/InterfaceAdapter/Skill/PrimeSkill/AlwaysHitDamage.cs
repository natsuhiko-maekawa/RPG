using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;

namespace BattleScene.InterfaceAdapter.Skill.PrimeSkill
{
    public class AlwaysHitDamage : AbstractDamage
    {
        public override HitEvaluationCode HitEvaluationCode { get; } = HitEvaluationCode.AlwaysHit;
    }
}