namespace BattleScene.Domain.ValueObject
{
    public record BattlePropertyValueObject(
        int SlipDefaultTurn,
        float SlipDefaultDamageRate,
        float IsHitThreshold,
        int MaxAgility);
}