using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public record DestroyValueObject(
        BodyPartCode BodyPartCode,
        float LuckRate,
        int Count);
}