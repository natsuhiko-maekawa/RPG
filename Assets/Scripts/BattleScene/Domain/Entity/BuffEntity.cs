using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.Entity
{
    public class BuffEntity : BaseEntity<(CharacterId, BuffCode)>
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
        public float Rate { get; }
        public bool TurnIsEnd => Turn < 0;

        public void AdvanceTurn()
        {
            ++Turn;
        }
    }
}