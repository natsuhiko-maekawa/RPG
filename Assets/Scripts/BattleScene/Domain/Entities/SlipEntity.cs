using BattleScene.Domain.Codes;

namespace BattleScene.Domain.Entities
{
    public partial class SlipEntity : BaseEntity<SlipCode>
    {
        public SlipEntity(
            SlipCode slipCode,
            int defaultTurn)
        {
            Id = slipCode;
            _defaultTurn = defaultTurn;
        }

        public override SlipCode Id { get; }
        public int Turn { get; private set; }
        private readonly int _defaultTurn;
        private bool _effects;

        public bool Effects
        {
            get => _effects;
            set
            {
                _effects = value;
                EffectsOnChange(value);
                Turn = _defaultTurn;
            }
        }

        partial void EffectsOnChange(bool value);

        public void AdvanceTurn()
        {
            --Turn;
            if (Turn < 0) Turn = _defaultTurn - 1;
        }
    }
}