using BattleScene.Presenters.SkillComponents.BaseClass;

namespace BattleScene.Presenters.SkillComponents
{
    public class Shichishitou : BaseDamage
    {
        public override int AttackCount { get; } = 7;
        public override float DamageRate { get; } = 1.0f / 7.0f;
    }
}