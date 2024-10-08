using LoadingScene.Domain;
using VContainer;
using VContainer.Unity;

namespace LoadingScene.UseCase
{
    public class LoadingSceneUseCaseLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ILoader, Loader.Loader>(Lifetime.Singleton);
            // builder.Register<ITips, Tips.Tips>(Lifetime.Singleton);
        }
    }
}