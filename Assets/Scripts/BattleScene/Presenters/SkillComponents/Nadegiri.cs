using BattleScene.Presenters.SkillComponents.BaseClass;

namespace BattleScene.Presenters.SkillComponents
{
    public class Nadegiri : BaseDamage
    {
        public override float DamageRate { get; } = 2.0f / 3.0f;
    }
}