using BattleScene.InterfaceAdapter.Interface;
using BattleScene.UseCases.View.TechnicalPointBarView.OutputBoundary;
using BattleScene.UseCases.View.TechnicalPointBarView.OutputData;

namespace BattleScene.InterfaceAdapter.Presenter.StatusBarView
{
    public class TechnicalPointBarViewPresenter : ITechnicalPointBarViewPresenter
    {
        private readonly IPlayerView _playerView;

        public TechnicalPointBarViewPresenter(
            IPlayerView playerView)
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