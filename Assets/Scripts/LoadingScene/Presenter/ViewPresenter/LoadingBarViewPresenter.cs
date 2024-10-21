using LoadingScene.UseCase.IPresenter;
using LoadingScene.View.View;

namespace LoadingScene.Presenter.ViewPresenter
{
    public class LoadingBarViewPresenter : ILoadingBarViewPresenter
    {
        private readonly LoadingBarView _loadingBarView;

        public LoadingBarViewPresenter(
            LoadingBarView loadingBarView)
        {
            _loadingBarView = loadingBarView;
        }

        public void StartLoadingBarView(float progress)
        {
            _loadingBarView.StartAnimation(progress);
        }
    }
}