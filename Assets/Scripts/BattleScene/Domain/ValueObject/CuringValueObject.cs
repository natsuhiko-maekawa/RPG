using BattleScene.Domain.Id;

namespace BattleScene.Domain.ValueObject
{
    public record CuringValueObject(
        int Amount,
        CharacterId TargetId);
}