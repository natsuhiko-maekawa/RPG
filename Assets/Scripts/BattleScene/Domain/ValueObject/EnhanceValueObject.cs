using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public record EnhanceValueObject(
        EnhanceCode EnhanceCode,
        int Turn,
        LifetimeCode LifetimeCode);
}