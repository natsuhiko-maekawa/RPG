using BattleScene.Domain.DomainServices;
using BattleScene.UseCases.IServices;

namespace BattleScene.UseCases.UseCases
{
    public class InitializePlayerUseCase
    {
        private readonly ICharacterCreatorService _characterCreator;
        private readonly PlayerDomainService _player;

        public InitializePlayerUseCase(
            ICharacterCreatorService characterCreator,
            PlayerDomainService player)
        {
            _characterCreator = characterCreator;
            _player = player;
        }

        public void Initialize()
        {
            _player.Add();
            var playerId = _player.Get().Id;
            _characterCreator.Create(playerId, isPlayer: true);
        }
    }
}