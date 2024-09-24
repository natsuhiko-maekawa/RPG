using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public record BodyPartPropertyValueObject(
        BodyPartCode BodyPartCode,
        int Count);
}