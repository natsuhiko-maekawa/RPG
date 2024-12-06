using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

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