using BattleScene.Domain.Id;

namespace BattleScene.Domain.ValueObject
{
    public struct CuringValueObject // 16 byte
    {
        public int Amount { get; }
        public CharacterId TargetId { get; }

        public CuringValueObject(
            int amount, 
            CharacterId targetId)
        {
            Amount = amount;
            TargetId = targetId;
        }
    }
}