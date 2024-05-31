using LoadingScene.InterfaceAdapter.Presenter;
using LoadingScene.InterfaceAdapter.Repository;
using LoadingScene.UseCase.IPresenter;
using LoadingScene.UseCase.IRepository;
using VContainer;
using VContainer.Unity;

namespace LoadingScene.InterfaceAdapter
{
    public class LoadingSceneControllerLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ISpriteRepository, SpriteRepository>(Lifetime.Singleton);
            builder.Register<ILoadingBarViewPresenter, LoadingBarViewPresenter>(Lifetime.Singleton);
            builder.Register<ISettingsRepository, SettingsRepository>(Lifetime.Singleton);
            builder.Register<ITipsRepository, TipsRepository>(Lifetime.Singleton);
            builder.Register<ITipsViewPresenter, TipsViewPresenter>(Lifetime.Singleton);
        }
    }
}