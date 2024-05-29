using System.Threading.Tasks;
using LoadingScene.Domain;
using LoadingScene.UseCase.IPresenter;
using LoadingScene.UseCase.IRepository;
using LoadingScene.UseCase.Tips;

namespace LoadingScene.UseCase.Loader
{
    public class Loader : ILoader
    {
        private readonly ITips _tips;
        private readonly ISpriteRepository _spriteRepository;
        private readonly ILoadingBarViewPresenter _loadingBarViewPresenter;
        private readonly ITipsViewPresenter _tipsViewPresenter;
        private readonly ISettingsRepository _settingsRepository;
        private int _addressableCount;

        public Loader(
            ITips tips,
            ISpriteRepository spriteRepository,
            ILoadingBarViewPresenter loadingBarViewPresenter,
            ITipsViewPresenter tipsViewPresenter,
            ISettingsRepository settingsRepository)
        {
            _tips = tips;
            _spriteRepository = spriteRepository;
            _loadingBarViewPresenter = loadingBarViewPresenter;
            _tipsViewPresenter = tipsViewPresenter;
            _settingsRepository = settingsRepository;
        }

        public async Task Awake()
        {
            _tipsViewPresenter.StartTipsView(_tips.RandomGet());
            _addressableCount = _settingsRepository.Get();
            _settingsRepository.Set(_addressableCount);
            await _spriteRepository.LoadImage();
        }

        public void Update()
        {
            var progress = _spriteRepository.GetProgress(_addressableCount);
            _loadingBarViewPresenter.StartLoadingBarView(progress);
        }
    }
}