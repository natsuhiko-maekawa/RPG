using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.SkillComponents
{
    public class RandomShot : BaseDamage
    {
        public override int AttackNumber { get; } = 10;
        public override float DamageRate { get; } = 0.5f;
        public override float HitRate { get; } = 0.5f;
    }
}