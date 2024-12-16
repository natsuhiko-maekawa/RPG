using BattleScene.Domain.Codes;
using BattleScene.Domain.Ids;

namespace BattleScene.Domain.Entities
{
    public class EnhanceEntity : BaseEntity<(CharacterId, EnhanceCode)>
    {
        public EnhanceEntity(
            CharacterId characterId,
            EnhanceCode enhanceCode)
        {
            CharacterId = characterId;
            EnhanceCode = enhanceCode;
        }

        public override (CharacterId, EnhanceCode) Id => (CharacterId, EnhanceCode);
        public CharacterId CharacterId { get; }
        public EnhanceCode EnhanceCode { get; }
        public LifetimeCode LifetimeCode { get; private set; }
        public bool Effects { get; private set; }
        private int _turn;

        public void Set(
            int turn,
            LifetimeCode lifetimeCode)
        {
            _turn = turn;
            LifetimeCode = lifetimeCode;
            Effects = true;
        }

        public void AdvanceTurn()
        {
            --_turn;
            if (_turn < 0 && Effects) Effects = false;
        }
    }
}