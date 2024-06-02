using Utility.Interface;
using VContainer;
using VContainer.Unity;

namespace Utility
{
    public class UtilityLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IRandomEx, RandomEx>(Lifetime.Singleton);
        }
    }
}