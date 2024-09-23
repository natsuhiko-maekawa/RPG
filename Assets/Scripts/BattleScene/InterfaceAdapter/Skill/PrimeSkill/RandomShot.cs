using BattleScene.InterfaceAdapter.Skill.AbstractClass;

namespace BattleScene.InterfaceAdapter.Skill.PrimeSkill
{
    public class RandomShot : AbstractDamage
    {
        public override int AttackNumber { get; } = 10;
        public override float DamageRate { get; } = 0.5f;
        public override float HitRate { get; } = 0.5f;
    }
}