using BattleScene.Domain.Id;

namespace BattleScene.Domain.ValueObject
{
    public record CureResultValueObject(
        int Amount,
        CharacterId TargetId);
}