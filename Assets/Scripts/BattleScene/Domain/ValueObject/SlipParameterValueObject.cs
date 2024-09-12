using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public record SlipParameterValueObject(
        SlipDamageCode SlipDamageCode,
        float DamageRate,
        DamageExpressionCode DamageExpressionCode,
        float LuckRate);
}