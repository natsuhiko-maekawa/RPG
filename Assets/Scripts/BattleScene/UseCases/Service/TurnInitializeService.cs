using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.UseCases.Service
{
    public class TurnInitializerService
    {
        private readonly ICollection<TurnEntity, TurnId> _turnCollection;

        public TurnInitializerService(
            ICollection<TurnEntity, TurnId> turnCollection)
        {
            _turnCollection = turnCollection;
        }

        public void Initialize()
        {
            var turnId = new TurnId();
            var turn = new TurnEntity(turnId, 0);
            _turnCollection.Add(turn);
        }
    }
}