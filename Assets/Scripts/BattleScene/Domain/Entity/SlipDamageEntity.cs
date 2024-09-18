using BattleScene.Domain.Code;

namespace BattleScene.Domain.Entity
{
    public class SlipDamageEntity : BaseEntity<SlipDamageCode>
    {
        public SlipDamageEntity(
            SlipDamageCode slipDamageCode,
            int turn)
        {
            Id = slipDamageCode;
            Turn = turn;
            DefaultTurn = turn;
        }
        
        public override SlipDamageCode Id { get; }
        public int Turn { get; private set; }
        private int DefaultTurn { get; }

        public void AdvanceTurn()
        {
            Turn--;
            if (Turn < 0) Turn = DefaultTurn;
        }
    }
}