using BattleScene.Domain.ValueObject;

namespace BattleScene.InterfaceAdapter.Skill.SkillElement
{
    public class RandomShot : AbstractDamage
    {
        public override int AttackNumber { get; } = 10;
        public override float DamageRate { get; } = 0.5f;
        public override float HitRate { get; } = 0.5f;
    }
}