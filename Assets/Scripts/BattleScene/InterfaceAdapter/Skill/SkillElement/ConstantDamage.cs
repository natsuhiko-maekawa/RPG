using BattleScene.Domain.ValueObject;

namespace BattleScene.InterfaceAdapter.Skill.SkillElement
{
    public class ConstantDamage : AbstractDamage
    {
        public override float DamageRate { get; } = 1.0f / 7.0f;
        public override DamageExpressionCode DamageExpressionCode { get; } = DamageExpressionCode.Constant;
    }
}