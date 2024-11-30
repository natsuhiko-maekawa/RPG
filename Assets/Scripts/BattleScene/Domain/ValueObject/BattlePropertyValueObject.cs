namespace BattleScene.Domain.ValueObject
{
    public record BattlePropertyValueObject(
        int SlipDefaultTurn,
        float SlipDefaultDamageRate,
        float IsHitThreshold,
        float AilmentSuccessThreshold,
        int MaxAgility,
        int MaxOrderCount,
        int AttackCountLimit);
}