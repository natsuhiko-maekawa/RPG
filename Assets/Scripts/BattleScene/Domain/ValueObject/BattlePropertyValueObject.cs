namespace BattleScene.Domain.ValueObject
{
    public class BattlePropertyValueObject
    {
        public int SlipDefaultTurn { get; }

        public BattlePropertyValueObject(
            int slipDefaultTurn)
        {
            SlipDefaultTurn = slipDefaultTurn;
        }
    }
}