using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public record SlipValueObject(
        SlipCode SlipCode,
        float DamageRate,
        DamageExpressionCode DamageExpressionCode,
        float LuckRate);
}