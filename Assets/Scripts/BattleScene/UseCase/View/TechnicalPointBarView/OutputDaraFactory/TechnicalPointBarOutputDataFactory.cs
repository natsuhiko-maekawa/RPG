using BattleScene.Domain.IRepository;
using BattleScene.UseCase.View.TechnicalPointBarView.OutputData;

namespace BattleScene.UseCase.View.TechnicalPointBarView.OutputDaraFactory
{
    public class TechnicalPointBarOutputDataFactory
    {
        private readonly ITechnicalPointRepository _technicalPointRepository;

        public TechnicalPointBarOutputData Create()
        {
            var technicalPointBarOutputData = new TechnicalPointBarOutputData(
                _technicalPointRepository.Select().GetMax(),
                _technicalPointRepository.Select().GetCurrent());
            return technicalPointBarOutputData;
        }
    }
}