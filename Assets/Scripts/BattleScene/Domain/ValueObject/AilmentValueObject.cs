using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public record AilmentValueObject(
        AilmentCode AilmentCode,
        float LuckRate);
}