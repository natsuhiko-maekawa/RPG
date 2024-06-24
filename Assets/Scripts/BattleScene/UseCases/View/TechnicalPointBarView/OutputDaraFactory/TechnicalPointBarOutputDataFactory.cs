using BattleScene.Domain.IRepository;
using BattleScene.UseCases.View.TechnicalPointBarView.OutputData;

namespace BattleScene.UseCases.View.TechnicalPointBarView.OutputDaraFactory
{
    public class TechnicalPointBarOutputDataFactory
    {
        private readonly ITechnicalPointRepository _technicalPointRepository;

        public TechnicalPointBarOutputDataFactory(ITechnicalPointRepository technicalPointRepository)
        {
            _technicalPointRepository = technicalPointRepository;
        }

        public TechnicalPointBarOutputData Create()
        {
            var technicalPointBarOutputData = new TechnicalPointBarOutputData(
                _technicalPointRepository.Select().GetMax(),
                _technicalPointRepository.Select().GetCurrent());
            return technicalPointBarOutputData;
        }
    }
}