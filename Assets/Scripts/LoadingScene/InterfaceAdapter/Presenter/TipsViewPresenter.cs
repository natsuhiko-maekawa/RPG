using LoadingScene.InterfaceAdapter.Presenter.IView;
using LoadingScene.UseCase.IPresenter;

namespace LoadingScene.InterfaceAdapter.Presenter
{
    public class TipsViewPresenter : ITipsViewPresenter
    {
        private readonly ITipsView _tipsView;

        public TipsViewPresenter(
            ITipsView tipsView)
        {
            _tipsView = tipsView;
        }

        public void StartTipsView(string tips)
        {
            _tipsView.StartAnimation(tips);
        }
    }
}