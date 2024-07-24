using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Skill.SkillElement
{
    public class Shichishitou : AbstractDamage
    {
        public override int AttackNumber { get; } = 7;
        public override float DamageRate { get; } = 1.0f / 7.0f;
    }
}