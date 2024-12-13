namespace BattleScene.Domain.Codes
{
    public enum DamageExpressionCode : byte
    {
        Basic,
        Constant,
        Slip
    }

    public enum HitEvaluationCode : byte
    {
        Basic,
        AlwaysHit
    }

    public enum AttacksWeakPointEvaluationCode : byte
    {
        Basic
    }
}