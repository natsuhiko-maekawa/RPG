using BattleScene.Domain.Codes;

namespace BattleScene.Domain.ValueObjects
{
    public record CureValueObject(
        CureExpressionCode CureExpressionCode,
        float Rate);
}