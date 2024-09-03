using BattleScene.Domain.Id;

namespace BattleScene.Domain.ValueObject
{
    public record RecoveryValueObject(
        int Amount,
        CharacterId TargetId);
}