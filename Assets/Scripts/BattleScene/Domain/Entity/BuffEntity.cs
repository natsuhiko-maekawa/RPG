using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.Entity
{
    public partial class BuffEntity : BaseEntity<(CharacterId, BuffCode)>
    {
        public BuffEntity(
            CharacterId characterId,
            BuffCode buffCode,
            int turn,
            LifetimeCode lifetimeCode,
            float rate = 1.0f)
        {
            CharacterId = characterId;
            BuffCode = buffCode;
            Turn = turn;
            LifetimeCode = lifetimeCode;
            Rate = rate;
        }

        public override (CharacterId, BuffCode) Id => (CharacterId, BuffCode);
        public CharacterId CharacterId { get; }
        public BuffCode BuffCode { get; }
        public int Turn { get; private set; }
        public LifetimeCode LifetimeCode { get; }

        private float _rate;

        public float Rate
        {
            get => _rate;
            private set
            {
                _rate = value;
                RateOnChange(_rate);
            }
        }

        partial void RateOnChange(float value);

        public bool TurnIsEnd => Turn < 0;

        public void AdvanceTurn()
        {
            --Turn;
            if (Turn < 0) Rate = 1.0f;
        }
    }
}