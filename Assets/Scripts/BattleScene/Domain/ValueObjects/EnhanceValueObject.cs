using BattleScene.Domain.Codes;

namespace BattleScene.Domain.ValueObjects
{
    public record EnhanceValueObject(
        EnhanceCode EnhanceCode,
        int Turn,
        LifetimeCode LifetimeCode);
}