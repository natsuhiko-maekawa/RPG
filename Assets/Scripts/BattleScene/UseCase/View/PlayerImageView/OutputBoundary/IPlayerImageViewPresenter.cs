using BattleScene.UseCase.View.PlayerImageView.OutputData;

namespace BattleScene.UseCase.View.PlayerImageView.OutputBoundary
{
    public interface IPlayerImageViewPresenter
    {
        public void Start(PlayerImageOutputData outputData);
    }
}