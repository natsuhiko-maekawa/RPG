using BattleScene.Domain.Codes;

namespace BattleScene.Domain.ValueObjects
{
    public record SlipValueObject(
        SlipCode SlipCode,
        float DamageRate,
        DamageExpressionCode DamageExpressionCode,
        float LuckRate);
}