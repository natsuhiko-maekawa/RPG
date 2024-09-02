using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public record BuffParameterValueObject(
        BuffCode BuffCode,
        float Rate,
        int Turn,
        LifetimeCode LifetimeCode);
}