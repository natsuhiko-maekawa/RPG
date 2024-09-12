using BattleScene.Domain.Id;

namespace BattleScene.Domain.Entity
{
    public class TurnEntity : BaseEntity<TurnEntity, TurnId>
    {
        public override TurnId Id { get; }
        public int Turn { get; private set; }

        public TurnEntity(
            TurnId id,
            int turn)
        {
            Id = id;
            Turn = turn;
        }
        
        public void Increment()
        {
            Turn++;
        }
    }
}