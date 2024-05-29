using BattleScene.Domain.Id;

namespace BattleScene.Domain.ValueObject
{
    public record CureValueObject(
        int Amount,
        CharacterId TargetId);
}