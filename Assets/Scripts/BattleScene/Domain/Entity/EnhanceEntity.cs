using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.Entity
{
    public class EnhanceEntity : BaseEntity<(CharacterId, EnhanceCode)>
    {
        public EnhanceEntity(
            CharacterId characterId,
            EnhanceCode enhanceCode,
            int turn,
            LifetimeCode lifetimeCode)
        {
            CharacterId = characterId;
            EnhanceCode = enhanceCode;
            Turn = turn;
            LifetimeCode = lifetimeCode;
            Effects = true;
        }

        public override (CharacterId, EnhanceCode) Id => (CharacterId, EnhanceCode);
        public CharacterId CharacterId { get; }
        public EnhanceCode EnhanceCode { get; }
        public int Turn { get; private set; }
        public LifetimeCode LifetimeCode { get; }
        public bool Effects { get; set; }

        public void AdvanceTurn()
        {
            Turn--;
            if (Turn < 0 && Effects) Effects = false;
        }
    }
}