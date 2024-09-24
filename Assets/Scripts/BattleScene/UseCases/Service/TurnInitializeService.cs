using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.UseCases.Service
{
    public class TurnInitializerService
    {
        private readonly IRepository<TurnEntity, TurnId> _turnRepository;

        public TurnInitializerService(
            IRepository<TurnEntity, TurnId> turnRepository)
        {
            _turnRepository = turnRepository;
        }

        public void Initialize()
        {
            var turnId = new TurnId();
            var turn = new TurnEntity(turnId, 0);
            _turnRepository.Update(turn);
        }
    }
}