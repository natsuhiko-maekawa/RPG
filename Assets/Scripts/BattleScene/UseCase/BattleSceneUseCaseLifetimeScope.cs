using BattleScene.Domain.DomainService;
using BattleScene.UseCase.Event;
using BattleScene.UseCase.Event.Runner;
using BattleScene.UseCase.Service;
using BattleScene.UseCase.View.AilmentView.OutputDataFactory;
using BattleScene.UseCase.View.AttackCountView.OutputDataFactory;
using BattleScene.UseCase.View.BuffView.OutputDataFactory;
using BattleScene.UseCase.View.CharacterVibesView.OutputDataFactory;
using BattleScene.UseCase.View.DestroyedPartView.OutputDataFactory;
using BattleScene.UseCase.View.DigitView.OutputDataFactory;
using BattleScene.UseCase.View.EnemyView.OutputDataFactory;
using BattleScene.UseCase.View.FrameView.OutputDataFactory;
using BattleScene.UseCase.View.HitPointBarView.OutputDataFactory;
using BattleScene.UseCase.View.IsContinueView.OutputDataFactory;
using BattleScene.UseCase.View.MessageView.OutputDataFactory;
using BattleScene.UseCase.View.OrderView.OutputDataFactory;
using BattleScene.UseCase.View.PlayerImageView.OutputDataFactory;
using BattleScene.UseCase.View.SelectActionView.OutputDataFactory;
using BattleScene.UseCase.View.SelectSkillView.OutputDataFactory;
using BattleScene.UseCase.View.TechnicalPointBarView.OutputDaraFactory;
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

            builder.Register<AilmentDomainService>(Lifetime.Singleton);
            builder.Register<BodyPartDomainService>(Lifetime.Singleton);
            builder.Register<BuffDomainService>(Lifetime.Singleton);
            builder.Register<CharactersDomainService>(Lifetime.Singleton);
            builder.Register<HitPointDomainService>(Lifetime.Singleton);
            builder.Register<OrderedItemsDomainService>(Lifetime.Singleton);
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
            builder.Register<OrderOutputDataFactory>(Lifetime.Singleton);
            builder.Register<AilmentPlayerImageOutputDataFactory>(Lifetime.Singleton);
            builder.Register<PlayerAttackPlayerImageOutputDataFactory>(Lifetime.Singleton);
            builder.Register<PlayerImageOutputDataFactory>(Lifetime.Singleton);
            builder.Register<SelectSkillPlayerImageOutputDataFactory>(Lifetime.Singleton);
            builder.Register<SelectActionOutputDataFactory>(Lifetime.Singleton);
            builder.Register<SelectSkillOutputDataFactory>(Lifetime.Singleton);
            builder.Register<TechnicalPointBarOutputDataFactory>(Lifetime.Singleton);
        }
    }
}