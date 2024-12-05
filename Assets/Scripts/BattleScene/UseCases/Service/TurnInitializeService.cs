using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.UseCases.Service
{
    public class TurnService
    {
        private readonly IRepository<TurnEntity, TurnId> _turnRepository;

        public TurnService(
            IRepository<TurnEntity, TurnId> turnRepository)
        {
            _turnRepository = turnRepository;
        }

        public void Initialize()
        {
            var turnId = new TurnId();
            var turn = new TurnEntity(turnId);
            _turnRepository.Add(turn);
        }

        public void Increment()
        {
            var turn = _turnRepository.Get()
                .Single();
            turn.Increment();
        }
    }
}