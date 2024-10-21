using LoadingScene.UseCase.IPresenter;
using LoadingScene.View.View;

namespace LoadingScene.Presenter.ViewPresenter
{
    public class TipsViewPresenter : ITipsViewPresenter
    {
        private readonly TipsView _tipsView;

        public TipsViewPresenter(
            TipsView tipsView)
        {
            _tipsView = tipsView;
        }

        public void StartTipsView(string tips)
        {
            _tipsView.StartAnimation(tips);
        }
    }
}