using BattleScene.Domain.Codes;

namespace BattleScene.Domain.ValueObjects
{
    public record BuffValueObject(
        BuffCode BuffCode,
        float Rate,
        int Turn,
        LifetimeCode LifetimeCode);
}