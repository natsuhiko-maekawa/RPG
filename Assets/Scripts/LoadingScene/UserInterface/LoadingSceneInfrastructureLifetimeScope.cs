using LoadingScene.InterfaceAdapter.Presenter.IView;
using LoadingScene.InterfaceAdapter.Repository.IAddressable;
using LoadingScene.InterfaceAdapter.Repository.IScriptableObject;
using LoadingScene.UserInterface.Addressable;
using VContainer;
using VContainer.Unity;

namespace LoadingScene.UserInterface
{
    public class LoadingSceneInfrastructureLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ISpriteAddressable, SpriteAddressable>(Lifetime.Singleton);
            builder.Register<IAddressableCount, AddressableCount>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<ILoadingBarView>();
            builder.RegisterComponentInHierarchy<ILoadingSceneScriptableObject>();
            builder.RegisterComponentInHierarchy<ITipsView>();
        }
    }
}