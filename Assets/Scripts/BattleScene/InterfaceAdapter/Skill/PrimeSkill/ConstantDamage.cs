using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;

namespace BattleScene.InterfaceAdapter.Skill.PrimeSkill
{
    public class ConstantDamage : AbstractDamage
    {
        public override float DamageRate { get; } = 1.0f / 7.0f;
        public override DamageExpressionCode DamageExpressionCode { get; } = DamageExpressionCode.Constant;
    }
}