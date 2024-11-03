using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public record CureValueObject(
        CureExpressionCode CureExpressionCode,
        float Rate);
}