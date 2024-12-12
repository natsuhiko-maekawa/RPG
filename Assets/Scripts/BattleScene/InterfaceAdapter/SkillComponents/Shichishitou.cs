using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.SkillComponents
{
    public class Shichishitou : BaseDamage
    {
        public override int AttackNumber { get; } = 7;
        public override float DamageRate { get; } = 1.0f / 7.0f;
    }
}