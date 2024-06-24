using BattleScene.UseCases.View.PlayerImageView.OutputData;

namespace BattleScene.UseCases.View.PlayerImageView.OutputBoundary
{
    public interface IPlayerImageViewPresenter
    {
        public void Start(PlayerImageOutputData outputData);
    }
}