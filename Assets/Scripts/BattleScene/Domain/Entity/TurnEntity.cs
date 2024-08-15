using BattleScene.Domain.OldId;

namespace BattleScene.Domain.Entity
{
    public class TurnEntity : BaseEntity<TurnEntity, TurnId>
    {
        private int _turn;

        public void Increment()
        {
            ++_turn;
        }

        public TurnNumber Get()
        {
            return new TurnNumber(_turn);
        }

        public override TurnId Id { get; }
        
        public int Turn { get; }

        public TurnEntity(
            TurnId id,
            int turn)
        {
            Id = id;
            Turn = turn;
        }
    }
}