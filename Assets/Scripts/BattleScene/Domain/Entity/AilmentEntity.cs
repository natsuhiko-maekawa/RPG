using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.Entity
{
    public class AilmentEntity
    {
        private TurnValueObject _turn;

        public AilmentEntity(
            CharacterId characterId,
            AilmentCode ailmentCode,
            Priority priority,
            TurnValueObject turn)
        {
            CharacterId = characterId;
            AilmentCode = ailmentCode;
            Priority = priority;
            _turn = turn;
        }

        public CharacterId CharacterId { get; }
        public AilmentCode AilmentCode { get; }
        public Priority Priority { get; }

        public int? GetTurn()
        {
            return _turn.Get();
        }

        public void AdvanceTurn()
        {
            _turn = _turn.Advance();
        }

        public bool TurnIsEnd()
        {
            return _turn.IsEnd();
        }

        public override bool Equals(object obj)
        {
            if (obj is not AilmentEntity ailmentEntity) return false;
            return CharacterId == ailmentEntity.CharacterId && AilmentCode == ailmentEntity.AilmentCode;
        }

        public override int GetHashCode()
        {
            return (CharacterId, AilmentCode).GetHashCode();
        }
    }
}