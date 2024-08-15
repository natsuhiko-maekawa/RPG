using BattleScene.Domain.DomainService;
using BattleScene.UseCases.Event;
using BattleScene.UseCases.OldEvent;
using BattleScene.UseCases.OldEvent.Runner;
using BattleScene.UseCases.Output;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.StateMachine.Service;
using BattleScene.UseCases.UseCase;
using BattleScene.UseCases.View.AilmentView;
using BattleScene.UseCases.View.AilmentView.OutputDataFactory;
using BattleScene.UseCases.View.AttackCountView.OutputDataFactory;
using BattleScene.UseCases.View.BuffView.OutputDataFactory;
using BattleScene.UseCases.View.CharacterVibesView.OutputDataFactory;
using BattleScene.UseCases.View.DestroyedPartView.OutputDataFactory;
using BattleScene.UseCases.View.DigitView.OutputDataFactory;
using BattleScene.UseCases.View.EnemyView;
using BattleScene.UseCases.View.EnemyView.OutputDataFactory;
using BattleScene.UseCases.View.FrameView.OutputDataFactory;
using BattleScene.UseCases.View.HitPointBarView.OutputDataFactory;
using BattleScene.UseCases.View.IsContinueView.OutputDataFactory;
using BattleScene.UseCases.View.MessageView.OutputDataFactory;
using BattleScene.UseCases.View.PlayerImageView.OutputDataFactory;
using BattleScene.UseCases.View.SelectActionView.OutputDataFactory;
using BattleScene.UseCases.View.SelectSkillView.OutputDataFactory;
using BattleScene.UseCases.View.TechnicalPointBarView.OutputDaraFactory;
using VContainer;
using VContainer.Unity;

namespace BattleScene.UseCases
{
    public class BattleSceneUseCasesLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<AilmentSkillOldEvent>(Lifetime.Singleton);
            builder.Register<AilmentsResetOldEvent>(Lifetime.Singleton);
            builder.Register<SlipDamageOldEvent>(Lifetime.Singleton);
            builder.Register<SlipDamageMessageOldEvent>(Lifetime.Singleton);
            builder.Register<BattleStart>(Lifetime.Singleton);
            builder.Register<BuffOldEvent>(Lifetime.Singleton);
            builder.Register<CantActionOldEvent>(Lifetime.Singleton);
            builder.Register<CureOldEvent>(Lifetime.Singleton);
            builder.Register<DestroyedPartOldEvent>(Lifetime.Singleton);
            builder.Register<EnemySelectSkill>(Lifetime.Singleton);
            builder.Register<EnemySuicideOldEvent>(Lifetime.Singleton);
            builder.Register<Initialization>(Lifetime.Singleton);
            builder.Register<IsContinueOldEvent>(Lifetime.Singleton);
            builder.Register<LoopEndOldEvent>(Lifetime.Singleton);
            builder.Register<OrderDecision>(Lifetime.Singleton);
            builder.Register<Attack>(Lifetime.Singleton);
            builder.Register<PlayerBeatEnemyOldEvent>(Lifetime.Singleton);
            builder.Register<PlayerDeadOldEvent>(Lifetime.Singleton);
            builder.Register<PlayerDamageOldEvent>(Lifetime.Singleton);
            builder.Register<PlayerWinOldEvent>(Lifetime.Singleton);
            builder.Register<ResetSkillOldEvent>(Lifetime.Singleton);
            builder.Register<SelectActionOldEvent>(Lifetime.Singleton);
            builder.Register<SelectFatalitySkillOldEvent>(Lifetime.Singleton);
            builder.Register<SelectSkillOldEvent>(Lifetime.Singleton);
            builder.Register<SelectTargetOldEvent>(Lifetime.Singleton);
            // builder.Register<Runner>(Lifetime.Singleton);
            // builder.Register<UseCaseFactory>(Lifetime.Singleton);
            // builder.Register<OutputFactory>(Lifetime.Singleton);
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
            builder.Register<HitPointCreatorService>(Lifetime.Singleton);
            builder.Register<OrderedItemCreatorService>(Lifetime.Singleton);
            builder.Register<ResetSkillService>(Lifetime.Singleton);
            builder.Register<SelectSkillService>(Lifetime.Singleton);
            builder.Register<SkillCreatorService>(Lifetime.Singleton);
            builder.Register<SkillService>(Lifetime.Singleton);
            builder.Register<SlipDamageService>(Lifetime.Singleton);
            builder.Register<ToBodyPartNumberService>(Lifetime.Singleton);
            builder.Register<ToBuffNumberService>(Lifetime.Singleton);
            // builder.Register<TurnService>(Lifetime.Singleton);

            builder.Register<AilmentDomainService>(Lifetime.Singleton);
            builder.Register<BodyPartDomainService>(Lifetime.Singleton);
            builder.Register<BuffDomainService>(Lifetime.Singleton);
            builder.Register<CharactersDomainService>(Lifetime.Singleton);
            builder.Register<EnemiesDomainService>(Lifetime.Singleton);
            builder.Register<HitPointDomainService>(Lifetime.Singleton);
            builder.Register<OrderedItemsDomainService>(Lifetime.Singleton);
            builder.Register<PlayerDomainService>(Lifetime.Singleton);
            builder.Register<ResultCreatorDomainService>(Lifetime.Singleton);
            builder.Register<ResultDomainService>(Lifetime.Singleton);
            builder.Register<SlipDamageDomainService>(Lifetime.Singleton);
            builder.Register<TargetDomainService>(Lifetime.Singleton);

            builder.Register<AilmentOutputDataFactory>(Lifetime.Singleton);
            builder.Register<AttackCountOutputDataFactory>(Lifetime.Singleton);
            builder.Register<BuffOutputDataFactory>(Lifetime.Singleton);
            builder.Register<CharacterVibesOutputDataFactory>(Lifetime.Singleton);
            builder.Register<DestroyedPartOutputDataFactory>(Lifetime.Singleton);
            builder.Register<CureDigitOutputDataFactory>(Lifetime.Singleton);
            builder.Register<DamageDigitOutputDataFactory>(Lifetime.Singleton);
            builder.Register<EnemyOutputDataFactory>(Lifetime.Singleton);
            builder.Register<TargetFrameOutputDataFactory>(Lifetime.Singleton);
            builder.Register<HitPointBarOutputDataFactory>(Lifetime.Singleton);
            builder.Register<IsContinueOutputDataFactory>(Lifetime.Singleton);
            builder.Register<AilmentMessageOutputDataFactory>(Lifetime.Singleton);
            builder.Register<DamageMessageOutputDataFactory>(Lifetime.Singleton);
            builder.Register<MessageOutputDataFactory>(Lifetime.Singleton);
            builder.Register<SelectSkillMessageOutputDataFactory>(Lifetime.Singleton);
            // builder.Register<OrderOutputDataFactory>(Lifetime.Singleton);
            builder.Register<AilmentPlayerImageOutputDataFactory>(Lifetime.Singleton);
            builder.Register<PlayerAttackPlayerImageOutputDataFactory>(Lifetime.Singleton);
            builder.Register<PlayerImageOutputDataFactory>(Lifetime.Singleton);
            builder.Register<SelectSkillPlayerImageOutputDataFactory>(Lifetime.Singleton);
            builder.Register<SelectActionOutputDataFactory>(Lifetime.Singleton);
            builder.Register<SelectSkillOutputDataFactory>(Lifetime.Singleton);
            builder.Register<TechnicalPointBarOutputDataFactory>(Lifetime.Singleton);

            builder.Register<AilmentViewOutput>(Lifetime.Singleton);
            builder.Register<EnemyViewOutput>(Lifetime.Singleton);
            // builder.Register<OrderViewOutput>(Lifetime.Singleton);

            builder.Register<StateMachine.StateMachine>(Lifetime.Singleton);
            builder.Register<StateCreatorService>(Lifetime.Singleton);
            // builder.Register<InitializeStateCreatorService>(Lifetime.Singleton);
            builder.Register<EnemySkillStateCreatorService>(Lifetime.Singleton);

            builder.Register<InitializationEvent>(Lifetime.Singleton);
            builder.Register<EnemyInitializerEvent>(Lifetime.Singleton);
            builder.Register<OrderEvent>(Lifetime.Singleton);
            builder.Register<EnemySkillSelectorEvent>(Lifetime.Singleton);
            builder.Register<SkillExecutorEvent>(Lifetime.Singleton);
            builder.Register<SkillIterator>(Lifetime.Singleton);
            builder.Register<EventExecutor>(Lifetime.Singleton);

            builder.Register<OrderView>(Lifetime.Singleton);
        }
    }
}