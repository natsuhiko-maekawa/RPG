using LoadingScene.InterfaceAdapter;
using VContainer;
using VContainer.Unity;

namespace Mediator
{
    public class MediatorLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            // builder.Register<ILoadingImage, LoadingImage>(Lifetime.Singleton);
        }
    }
}