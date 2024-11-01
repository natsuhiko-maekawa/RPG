using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public record EnhanceParameterValueObject(
        EnhanceCode EnhanceCode,
        int Turn,
        LifetimeCode LifetimeCode);
}