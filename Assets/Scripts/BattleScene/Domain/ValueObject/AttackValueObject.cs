using BattleScene.Domain.OldId;

namespace BattleScene.Domain.ValueObject
{
    public class AttackValueObject
    {
        public AttackValueObject(
            int amount,
            CharacterId targetId,
            bool isHit,
            bool attacksWeakPoint,
            int number)
        {
            Amount = amount;
            TargetId = targetId;
            IsHit = isHit;
            AttacksWeakPoint = attacksWeakPoint;
            Number = number;
        }

        public AttackValueObject(
            int amount,
            CharacterId targetId)
        {
            Amount = amount;
            TargetId = targetId;
            IsHit = true;
            AttacksWeakPoint = false;
            Number = 0;
        }

        public int Amount { get; }
        public CharacterId TargetId { get; }
        public bool IsHit { get; }
        public bool AttacksWeakPoint { get; }
        public int Number { get; }
    }
}