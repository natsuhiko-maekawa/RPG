using BattleScene.InterfaceAdapter.DataAccess.IResource;
using BattleScene.InterfaceAdapter.IInputSystem;
using BattleScene.InterfaceAdapter.IView;
using VContainer;
using VContainer.Unity;

namespace BattleScene.Framework
{
    public class BattleSceneFrameworkLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<IEnemiesView>();
            builder.RegisterComponentInHierarchy<IInfoView>();
            builder.RegisterComponentInHierarchy<IBattleSceneInputSystem>();
            builder.RegisterComponentInHierarchy<IGridView>();
            builder.RegisterComponentInHierarchy<IMessageView>();
            builder.RegisterComponentInHierarchy<IOrderView>();
            builder.RegisterComponentInHierarchy<IPlayerAttackCountView>();
            builder.RegisterComponentInHierarchy<IPlayerView>();
            builder.RegisterComponentInHierarchy<IPlayerStatusView>();
            builder.RegisterComponentInHierarchy<ISelectActionView>();
            builder.RegisterComponentInHierarchy<ISelectSkillView>();
            builder.RegisterComponentInHierarchy<IPlayerPropertyResource>();
            builder.RegisterComponentInHierarchy<IPropertyResource>();
        }
    }
}