using LoadingScene.InterfaceAdapter;
using LoadingScene.UserInterface;
using VContainer;
using VContainer.Unity;

namespace Mediator
{
    public class MediatorLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.Register<ILoadingImage, LoadingImage>(Lifetime.Singleton);
            builder.Register<ISpriteFlyweight, SpriteFlyweight>(Lifetime.Singleton);
            builder.Register<BattleScene.UserInterface.ISpriteFlyweight, BattleScene.UserInterface.SpriteFlyweight>(
                Lifetime.Singleton);
            // builder.Register<ILoadingImage, BattleScene.Adapter.LoadingImage>(
            //     Lifetime.Singleton);
        }
    }
}