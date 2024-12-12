using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.SkillComponents
{
    public class Nadegiri : BaseDamage
    {
        public override float DamageRate { get; } = 2.0f / 3.0f;
    }
}