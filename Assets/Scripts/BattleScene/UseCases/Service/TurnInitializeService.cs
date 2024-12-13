using System.Linq;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.Ids;

namespace BattleScene.UseCases.Service
{
    public class TurnService
    {
        private readonly IRepository<TurnEntity, TurnId> _turnRepository;
        private readonly TurnId _turnId;

        public TurnService(
            IRepository<TurnEntity, TurnId> turnRepository)
        {
            _turnRepository = turnRepository;
            _turnId = new TurnId();
        }

        public void Initialize()
        {
            var turn = new TurnEntity(_turnId);
            _turnRepository.Add(turn);
        }

        public int GetCurrent()
        {
            var turn = _turnRepository.Get(_turnId).Turn;
            return turn;
        }

        public void Increment()
        {
            var turn = _turnRepository.Get()
                .Single();
            turn.Increment();
        }
    }
}