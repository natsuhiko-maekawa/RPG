using BattleScene.InterfaceAdapter.Skill.AbstractClass;

namespace BattleScene.InterfaceAdapter.Skill.PrimeSkill
{
    public class FiveTimeDamage : AbstractDamage
    {
        public override int AttackNumber { get; } = 5;
        public override float DamageRate { get; } = 1.0f / 5.0f;
    }
}