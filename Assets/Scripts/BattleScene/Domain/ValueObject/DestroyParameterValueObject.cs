using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public record DestroyParameterValueObject(
        BodyPartCode BodyPartCode,
        float LuckRate,
        int Count);
}