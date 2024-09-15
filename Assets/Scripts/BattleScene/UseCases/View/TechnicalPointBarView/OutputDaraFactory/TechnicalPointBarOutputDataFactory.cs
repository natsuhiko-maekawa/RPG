using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.View.TechnicalPointBarView.OutputData;

namespace BattleScene.UseCases.View.TechnicalPointBarView.OutputDaraFactory
{
    public class TechnicalPointBarOutputDataFactory
    {
        private readonly PlayerDomainService _player;
        private readonly IFactory<PlayerPropertyValueObject, CharacterTypeCode> _playerPropertyFactory;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;

        public TechnicalPointBarOutputDataFactory(
            PlayerDomainService player,
            IFactory<PlayerPropertyValueObject, CharacterTypeCode> playerPropertyFactory,
            IRepository<CharacterEntity, CharacterId> characterRepository)
        {
            _player = player;
            _playerPropertyFactory = playerPropertyFactory;
            _characterRepository = characterRepository;
        }

        public TechnicalPointBarOutputData Create()
        {
            var playerId = _player.GetId();
            var technicalPointBarOutputData = new TechnicalPointBarOutputData(
                _playerPropertyFactory.Create(CharacterTypeCode.Player).TechnicalPoint,
                _characterRepository.Select(playerId).CurrentTechnicalPoint);
            return technicalPointBarOutputData;
        }
    }
}