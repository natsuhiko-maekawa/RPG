using BattleScene.Domain.Codes;

namespace BattleScene.Domain.ValueObjects
{
    public record BodyPartPropertyValueObject(
        BodyPartCode BodyPartCode,
        int Count);
}