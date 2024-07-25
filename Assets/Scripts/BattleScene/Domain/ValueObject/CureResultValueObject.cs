using BattleScene.Domain.OldId;

namespace BattleScene.Domain.ValueObject
{
    public record CureResultValueObject(
        int Amount,
        CharacterId TargetId);
}