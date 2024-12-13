using BattleScene.Presenters.SkillComponents.BaseClass;

namespace BattleScene.Presenters.SkillComponents
{
    public class FiveTimeDamage : BaseDamage
    {
        public override int AttackNumber { get; } = 5;
        public override float DamageRate { get; } = 1.0f / 5.0f;
    }
}