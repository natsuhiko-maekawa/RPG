using BattleScene.Domain.OldId;

namespace BattleScene.Domain.ValueObject
{
    public record CureValueObject(
        int Amount,
        CharacterId TargetId);
}