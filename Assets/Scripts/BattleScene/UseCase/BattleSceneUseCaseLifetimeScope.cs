using BattleScene.UseCase.Event;
using VContainer;
using VContainer.Unity;

namespace BattleScene.UseCase
{
    public class BattleSceneUseCaseLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<AilmentSkillEvent>(Lifetime.Singleton);
            builder.Register<AilmentsResetEvent>(Lifetime.Singleton);
            builder.Register<SlipDamageEvent>(Lifetime.Singleton);
            builder.Register<SlipDamageMessageEvent>(Lifetime.Singleton);
            builder.Register<BuffEvent>(Lifetime.Singleton);
            builder.Register<CantActionEvent>(Lifetime.Singleton);
            builder.Register<CureEvent>(Lifetime.Singleton);
            builder.Register<DestroyedPartEvent>(Lifetime.Singleton);
            builder.Register<EnemySelectSkillEvent>(Lifetime.Singleton);
            builder.Register<EnemySuicideEvent>(Lifetime.Singleton);
            builder.Register<InitializationEvent>(Lifetime.Singleton);
            builder.Register<IsContinueEvent>(Lifetime.Singleton);
            builder.Register<LoopEndEvent>(Lifetime.Singleton);
            builder.Register<OrderDecisionEvent>(Lifetime.Singleton);
            builder.Register<PlayerAttackEvent>(Lifetime.Singleton);
            builder.Register<PlayerBeatEnemyEvent>(Lifetime.Singleton);
            builder.Register<PlayerDeadEvent>(Lifetime.Singleton);
            builder.Register<PlayerDamageEvent>(Lifetime.Singleton);
            builder.Register<PlayerWinEvent>(Lifetime.Singleton);
            builder.Register<ResetSkillEvent>(Lifetime.Singleton);
            builder.Register<SelectActionEvent>(Lifetime.Singleton);
            builder.Register<SelectFatalitySkillEvent>(Lifetime.Singleton);
            builder.Register<SelectSkillEvent>(Lifetime.Singleton);
            builder.Register<SelectTargetEvent>(Lifetime.Singleton);
        }
    }
}