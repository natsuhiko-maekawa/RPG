using BattleScene.Domain.Entity;

namespace BattleScene.Domain.ValueObject
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