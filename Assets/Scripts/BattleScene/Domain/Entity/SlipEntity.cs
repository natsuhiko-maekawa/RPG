using BattleScene.Domain.Code;

namespace BattleScene.Domain.Entity
{
    public partial class SlipEntity : BaseEntity<SlipCode>
    {
        public SlipEntity(
            SlipCode slipCode,
            bool effects = false,
            int turn = 0)
        {
            Id = slipCode;
            Effects = effects;
            Turn = turn;
            DefaultTurn = turn - 1;
        }

        public override SlipCode Id { get; }
        public int Turn { get; private set; }
        private int DefaultTurn { get; }

        private bool _effects;

        public bool Effects
        {
            get => _effects;
            set
            {
                _effects = value;
                EffectsOnChange(value);
            }
        }

        partial void EffectsOnChange(bool value);

        public void AdvanceTurn()
        {
            Turn--;
            if (Turn < 0) Turn = DefaultTurn;
        }
    }
}