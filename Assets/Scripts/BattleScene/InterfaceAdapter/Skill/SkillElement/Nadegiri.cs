using BattleScene.Domain.ValueObject;

namespace BattleScene.InterfaceAdapter.Skill.SkillElement
{
    public class Nadegiri : AbstractDamage
    {
        public override float DamageRate { get; } = 2.0f / 3.0f;
    }
}