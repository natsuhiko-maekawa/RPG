using BattleScene.Domain.Entities;

namespace BattleScene.Domain.ValueObjects
{
    public struct AttackValueObject // 16 byte
    {
        public AttackValueObject(
            int amount,
            CharacterEntity target,
            bool isHit,
            bool attacksWeakPoint,
            sbyte index)
        {
            Amount = amount;
            Target = target;
            IsHit = isHit;
            AttacksWeakPoint = attacksWeakPoint;
            Index = index;
        }

        public int Amount { get; }
        public CharacterEntity Target { get; }
        public bool IsHit { get; }
        public bool AttacksWeakPoint { get; }
        public sbyte Index { get; }
    }
}