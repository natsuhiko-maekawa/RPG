using LoadingScene.InterfaceAdapter.Presenter.IView;
using LoadingScene.UseCase.IPresenter;

namespace LoadingScene.InterfaceAdapter.Presenter
{
    public class LoadingBarViewPresenter : ILoadingBarViewPresenter
    {
        private readonly ILoadingBarView _loadingBarView;

        public LoadingBarViewPresenter(
            ILoadingBarView loadingBarView)
        {
            _loadingBarView = loadingBarView;
        }

        public void StartLoadingBarView(float progress)
        {
            _loadingBarView.StartAnimation(progress);
        }
    }
}