using BattleScene.Domain.Codes;

namespace BattleScene.Domain.ValueObjects
{
    public record DestroyValueObject(
        BodyPartCode BodyPartCode,
        float LuckRate,
        int Count);
}