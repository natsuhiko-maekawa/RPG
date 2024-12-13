using BattleScene.Domain.Entities;

namespace BattleScene.Domain.ValueObjects
{
    public struct CuringValueObject // 16 byte
    {
        public int Amount { get; }
        public CharacterEntity Target { get; }

        public CuringValueObject(
            int amount, 
            CharacterEntity target)
        {
            Amount = amount;
            Target = target;
        }
    }
}