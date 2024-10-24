using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.UseCases.Service
{
    public class TurnService
    {
        private readonly ICollection<TurnEntity, TurnId> _turnCollection;

        public TurnService(
            ICollection<TurnEntity, TurnId> turnCollection)
        {
            _turnCollection = turnCollection;
        }

        public void Initialize()
        {
            var turnId = new TurnId();
            var turn = new TurnEntity(turnId);
            _turnCollection.Add(turn);
        }

        public void Increment()
        {
            var turn = _turnCollection.Get()
                .Single();
            turn.Increment();
        }
    }
}