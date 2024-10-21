using LoadingScene.DataAccess.ScriptableObjects;
using LoadingScene.Presenter.ViewPresenter;
using LoadingScene.UseCase.IPresenter;
using LoadingScene.UseCase.IService;
using LoadingScene.UseCase.Service;
using LoadingScene.View.View;
using VContainer;
using VContainer.Unity;

namespace LoadingScene
{
    public class LoadingSceneDomainLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<LoadingBarView>();
            builder.RegisterComponentInHierarchy<LoadingSceneScriptableObject>();
            builder.RegisterComponentInHierarchy<TipsView>();
            builder.Register<ILoadingBarViewPresenter, LoadingBarViewPresenter>(Lifetime.Singleton);
            builder.Register<ITipsViewPresenter, TipsViewPresenter>(Lifetime.Singleton);
            builder.Register<ILoader, Loader>(Lifetime.Singleton);
            builder.Register<ITips, Tips>(Lifetime.Singleton);
        }
    }
}