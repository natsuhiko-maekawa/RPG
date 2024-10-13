using BattleScene.Domain.Id;

namespace BattleScene.Domain.ValueObject
{
    public class AttackValueObject
    {
        public AttackValueObject(
            int amount,
            CharacterId targetId,
            bool isHit,
            bool attacksWeakPoint,
            int index)
        {
            Amount = amount;
            TargetId = targetId;
            IsHit = isHit;
            AttacksWeakPoint = attacksWeakPoint;
            Index = index;
        }

        public int Amount { get; }
        public CharacterId TargetId { get; }
        public bool IsHit { get; }
        public bool AttacksWeakPoint { get; }
        public int Index { get; }
    }
}