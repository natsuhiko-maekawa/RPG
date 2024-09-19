using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.Entity
{
    public partial class AilmentEntity : BaseEntity<(CharacterId, AilmentCode)>
    {
        public AilmentEntity(
            CharacterId characterId,
            AilmentCode ailmentCode,
            bool effects,
            int turn,
            bool isSelfRecovery)
        {
            CharacterId = characterId;
            AilmentCode = ailmentCode;
            Effects = effects;
            Turn = turn;
            IsSelfRecovery = isSelfRecovery;
        }

        public override (CharacterId, AilmentCode) Id => (CharacterId, AilmentCode);
        public CharacterId CharacterId { get; }
        public AilmentCode AilmentCode { get; }
        public int Turn { get; private set; }
        public bool IsSelfRecovery { get; }
        public bool TurnIsEnd
        {
            get
            {
                if (!IsSelfRecovery) return false;
                return Turn < 0;
            }
        }

        private bool _effects;

        public bool Effects
        {
            get => _effects;
            set { _effects = value; EffectsOnChange(value); }
        }
        
        partial void EffectsOnChange(bool value);

        public void AdvanceTurn()
        {
            if (!IsSelfRecovery) return;
            if (Turn <= 0) return;
            Turn--;
        }
    }
}