using LoadingScene.UseCase.IPresenter;
using LoadingScene.UseCase.IService;

namespace LoadingScene.UseCase.Service
{
    public class Loader : ILoader
    {
        private readonly ILoadingBarViewPresenter _loadingBarViewPresenter;
        private readonly ITips _tips;
        private readonly ITipsViewPresenter _tipsViewPresenter;
        private int _addressableCount;

        public Loader(
            ITips tips,
            ILoadingBarViewPresenter loadingBarViewPresenter,
            ITipsViewPresenter tipsViewPresenter)
        {
            _tips = tips;
            _loadingBarViewPresenter = loadingBarViewPresenter;
            _tipsViewPresenter = tipsViewPresenter;
        }

        public void Awake()
        {
            _tipsViewPresenter.StartTipsView(_tips.RandomGet());
        }

        public void Update()
        {
            // 仮の値を設定している
            _loadingBarViewPresenter.StartLoadingBarView(0.0f);
        }
    }
}