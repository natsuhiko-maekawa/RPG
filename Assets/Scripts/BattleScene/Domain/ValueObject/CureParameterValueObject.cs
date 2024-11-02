using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public record CureParameterValueObject(
        CureExpressionCode CureExpressionCode,
        float Rate);
}