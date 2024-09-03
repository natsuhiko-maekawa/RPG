using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public record DestroyedParameterValueObject(
        BodyPartCode BodyPartCode,
        float LuckRate,
        int Count);
}