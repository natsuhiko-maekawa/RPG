using BattleScene.Domain.Aggregate;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;
using BattleScene.UseCases.View.TechnicalPointBarView.OutputData;

namespace BattleScene.UseCases.View.TechnicalPointBarView.OutputDaraFactory
{
    public class TechnicalPointBarOutputDataFactory
    {
        private readonly CharactersDomainService _characters;
        private readonly IRepository<TechnicalPointAggregate, CharacterId> _technicalPointRepository;

        public TechnicalPointBarOutputDataFactory(
            CharactersDomainService characters,
            IRepository<TechnicalPointAggregate, CharacterId> technicalPointRepository)
        {
            _characters = characters;
            _technicalPointRepository = technicalPointRepository;
        }

        public TechnicalPointBarOutputData Create()
        {
            var playerId = _characters.GetPlayerId();
            var technicalPointBarOutputData = new TechnicalPointBarOutputData(
                _technicalPointRepository.Select(playerId).GetMax(),
                _technicalPointRepository.Select(playerId).GetCurrent());
            return technicalPointBarOutputData;
        }
    }
}