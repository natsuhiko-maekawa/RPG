using BattleScene.Domain.Code;

namespace BattleScene.Domain.Entity
{
    public partial class SlipEntity : BaseEntity<SlipDamageCode>
    {
        public SlipEntity(
            SlipDamageCode slipDamageCode,
            bool effects,
            int turn)
        {
            Id = slipDamageCode;
            Effects = effects;
            Turn = turn;
            DefaultTurn = turn;
        }
        
        public override SlipDamageCode Id { get; }
        public int Turn { get; private set; }
        private int DefaultTurn { get; }
        
        private bool _effects;

        public bool Effects
        {
            get => _effects;
            set { _effects = value; EffectsOnChange(value); }
        }
        
        partial void EffectsOnChange(bool value);

        public void AdvanceTurn()
        {
            Turn--;
            if (Turn < 0) Turn = DefaultTurn;
        }
    }
}