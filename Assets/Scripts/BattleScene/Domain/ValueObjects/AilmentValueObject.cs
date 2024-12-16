using BattleScene.Domain.Codes;

namespace BattleScene.Domain.ValueObjects
{
    public record AilmentValueObject(
        AilmentCode AilmentCode,
        float LuckRate);
}