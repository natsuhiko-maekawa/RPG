using BattleScene.Domain.DomainService;
using BattleScene.UseCase.Event;
using BattleScene.UseCase.Event.Runner;
using BattleScene.UseCase.Service;
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
            builder.Register<BattleStartEvent>(Lifetime.Singleton);
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
            builder.Register<EventRunner>(Lifetime.Singleton);
            builder.Register<IEventFactory, EventFactory>(Lifetime.Singleton);
            builder.Register<ActionTimeCreatorService>(Lifetime.Singleton);
            builder.Register<AgilityToSpeedService>(Lifetime.Singleton);
            builder.Register<AilmentSkillService>(Lifetime.Singleton);
            builder.Register<AttackCounterService>(Lifetime.Singleton);
            builder.Register<CharacterCreatorService>(Lifetime.Singleton);
            builder.Register<CharacterOutputDataCreatorService>(Lifetime.Singleton);
            builder.Register<CureSkillService>(Lifetime.Singleton);
            builder.Register<DamageSkillService>(Lifetime.Singleton);
            builder.Register<DestroyedPartCreatorService>(Lifetime.Singleton);
            builder.Register<OrderedItemCreatorService>(Lifetime.Singleton);
            builder.Register<ResetSkillService>(Lifetime.Singleton);
            builder.Register<SelectSkillService>(Lifetime.Singleton);
            builder.Register<SkillCreatorService>(Lifetime.Singleton);
            builder.Register<SkillService>(Lifetime.Singleton);
            builder.Register<SlipDamageService>(Lifetime.Singleton);
            builder.Register<ToAilmentNumberService>(Lifetime.Singleton);
            builder.Register<ToBodyPartNumberService>(Lifetime.Singleton);
            builder.Register<ToBuffNumberService>(Lifetime.Singleton);
            builder.Register<TurnService>(Lifetime.Singleton);
            builder.Register<CharactersDomainService>(Lifetime.Singleton);

        }
    }
}