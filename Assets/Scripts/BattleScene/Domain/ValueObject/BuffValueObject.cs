using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public record BuffValueObject(
        BuffCode BuffCode,
        float Rate,
        int Turn,
        LifetimeCode LifetimeCode);
}