using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public record DestroyedPartParameterValueObject(
        BodyPartCode BodyPartCode,
        float LuckRate,
        int Count);
}