using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;

namespace BattleScene.InterfaceAdapter.PrimeSkill
{
    public class FiveTimeDamage : BaseDamage
    {
        public override int AttackNumber { get; } = 5;
        public override float DamageRate { get; } = 1.0f / 5.0f;
    }
}