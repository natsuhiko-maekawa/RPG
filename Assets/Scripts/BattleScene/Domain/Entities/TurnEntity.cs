using BattleScene.Domain.Ids;

namespace BattleScene.Domain.Entities
{
    public class TurnEntity : BaseEntity<TurnId>
    {
        public override TurnId Id { get; }
        public int Turn { get; private set; }

        public TurnEntity(
            TurnId id,
            int turn = 0)
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