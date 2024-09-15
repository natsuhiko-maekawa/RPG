using VContainer;
using VContainer.Unity;

namespace BattleScene.UseCases
{
    /// <summary>
    /// InterfaceAdapter層とUseCases層を統合するまでの仮のコンテナ
    /// </summary>
    public class BattleSceneLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
        }
    }
}