using BattleScene.Domain.Code;
using BattleScene.Domain.OldId;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.Entity
{
    public class BuffEntity : BaseEntity<BuffEntity, BuffId>
    {
        private readonly TurnValueObject _turn;

        public BuffEntity(
            CharacterId characterId,
            BuffCode buffCode,
            TurnValueObject turn,
            float rate = 1.0f)
        {
            CharacterId = characterId;
            BuffCode = buffCode;
            Rate = rate;
            _turn = turn;
        }

        public override BuffId Id => new(CharacterId, BuffCode);
        public CharacterId CharacterId { get; }
        public BuffCode BuffCode { get; }
        public float Rate { get; }

        public void AdvanceTurn()
        {
            _turn.Advance();
        }

        public bool TurnIsEnd()
        {
            return _turn.IsEnd();
        }
    }
}