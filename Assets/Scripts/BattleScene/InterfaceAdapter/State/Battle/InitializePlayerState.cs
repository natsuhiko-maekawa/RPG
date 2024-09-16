using BattleScene.Domain.DomainService;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    internal class InitializePlayerState : AbstractState
    {
        private readonly PlayerDomainService _player;
        private readonly InitializeEnemyState _initializeEnemyState;

        public InitializePlayerState(
            PlayerDomainService player,
            InitializeEnemyState initializeEnemyState)
        {
            _player = player;
            _initializeEnemyState = initializeEnemyState;
        }

        public override void Start()
        {
            _player.Add();
            Context.TransitionTo(_initializeEnemyState);
        }
    }
}