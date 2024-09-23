using BattleScene.InterfaceAdapter.Skill.AbstractClass;

namespace BattleScene.InterfaceAdapter.Skill.PrimeSkill
{
    public class Shichishitou : AbstractDamage
    {
        public override int AttackNumber { get; } = 7;
        public override float DamageRate { get; } = 1.0f / 7.0f;
    }
}