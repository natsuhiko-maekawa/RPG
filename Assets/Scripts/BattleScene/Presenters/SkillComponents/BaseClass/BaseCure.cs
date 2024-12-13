using BattleScene.Domain.Codes;

namespace BattleScene.Presenters.SkillComponents.BaseClass
{
    public abstract class BaseCure
    {
        public virtual float Rate { get; } = 1.0f;
        public virtual CureExpressionCode CureExpressionCode { get; } = CureExpressionCode.Basic;
    }
}