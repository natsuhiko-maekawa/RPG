using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using VContainer;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class InitializeBattleState : AbstractState
    {
        private readonly IObjectResolver _container;
        private readonly IRepository<TurnEntity, TurnId> _turnRepository;

        public InitializeBattleState(
            IObjectResolver container,
            IRepository<TurnEntity, TurnId> turnRepository)
        {
            _container = container;
            _turnRepository = turnRepository;
        }

        public override void Start()
        {
            SetTurn();
            Context.TransitionTo(_container.Resolve<InitializePlayerState>());
        }

        private void SetTurn()
        {
            var turnId = new TurnId();
            var turn = new TurnEntity(turnId, 0);
            _turnRepository.Update(turn);
        }
    }
}