namespace BattleScene.Domain.ValueObject
{
    public abstract class AbstractCure
    {
        public virtual float CureRate { get; } = 1.0f;
        public virtual CureExpressionCode CureExpressionCode { get; } = CureExpressionCode.Basic;
    }

    public enum CureExpressionCode
    {
        Basic
    }
}