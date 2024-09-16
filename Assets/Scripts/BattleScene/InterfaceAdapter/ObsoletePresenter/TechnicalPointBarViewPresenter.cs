using BattleScene.Framework.ViewModel;
using BattleScene.UseCases.View.TechnicalPointBarView.OutputBoundary;
using BattleScene.UseCases.View.TechnicalPointBarView.OutputData;

namespace BattleScene.InterfaceAdapter.ObsoletePresenter
{
    public class TechnicalPointBarViewPresenter : ITechnicalPointBarViewPresenter
    {
        private readonly Framework.View.PlayerView _playerView;

        public TechnicalPointBarViewPresenter(
            Framework.View.PlayerView playerView)
        {
            _playerView = playerView;
        }

        public void Start(TechnicalPointBarOutputData outputData)
        {
            var maxTechnicalPoint = outputData.MaxTechnicalPoint;
            var currentTechnicalPoint = outputData.CurrentTechnicalPoint;
            _playerView.StartPlayerTpBarView(
                new PlayerTpBarViewDto(new StatusBarViewDto(maxTechnicalPoint, currentTechnicalPoint)));
        }
    }
}