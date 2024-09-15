namespace BattleScene.Domain.ValueObject
{
    public class BattlePropertyValueObject
    {
        public int SlipDefaultTurn { get; }
        public float SlipDefalutDamageRate { get; }

        public BattlePropertyValueObject(
            int slipDefaultTurn,
            float slipDefaultDamageRate)
        {
            SlipDefaultTurn = slipDefaultTurn;
            SlipDefalutDamageRate = slipDefaultDamageRate;
        }
    }
}