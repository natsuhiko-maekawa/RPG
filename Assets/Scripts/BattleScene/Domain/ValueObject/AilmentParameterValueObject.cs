using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public record AilmentParameterValueObject(
        AilmentCode AilmentCode,
        float LuckRate);
}