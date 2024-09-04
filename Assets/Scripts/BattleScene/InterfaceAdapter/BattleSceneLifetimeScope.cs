﻿using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DataAccess.ObsoleteIFactory;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.DataAccess;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using BattleScene.InterfaceAdapter.DataAccess.Factory;
using BattleScene.InterfaceAdapter.DataAccess.Repository;
using BattleScene.InterfaceAdapter.DataAccess.Resource;
using BattleScene.InterfaceAdapter.IInput;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.InterfaceAdapter.Presenter.AilmentsView;
using BattleScene.InterfaceAdapter.Presenter.BuffView;
using BattleScene.InterfaceAdapter.Presenter.CharacterVibesView;
using BattleScene.InterfaceAdapter.Presenter.DestroyedPartView;
using BattleScene.InterfaceAdapter.Presenter.DigitView;
using BattleScene.InterfaceAdapter.Presenter.EnemyView;
using BattleScene.InterfaceAdapter.Presenter.FrameView;
using BattleScene.InterfaceAdapter.Presenter.GridView;
using BattleScene.InterfaceAdapter.Presenter.InfoView;
using BattleScene.InterfaceAdapter.Presenter.MessageView;
using BattleScene.InterfaceAdapter.Presenter.OrderView;
using BattleScene.InterfaceAdapter.Presenter.PlayerAttackCountView;
using BattleScene.InterfaceAdapter.Presenter.PlayerView;
using BattleScene.InterfaceAdapter.Presenter.SelectActionView;
using BattleScene.InterfaceAdapter.Presenter.SelectSkillView;
using BattleScene.InterfaceAdapter.Presenter.StatusBarView;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.InterfaceAdapter.Skill;
using BattleScene.InterfaceAdapter.Skill.SkillElement;
using BattleScene.InterfaceAdapter.State.Battle;
using BattleScene.InterfaceAdapter.State.Skill;
using BattleScene.UseCases.IPresenter;
using BattleScene.UseCases.Output;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.UseCase;
using BattleScene.UseCases.View;
using BattleScene.UseCases.View.AilmentView.OutputBoundary;
using BattleScene.UseCases.View.AttackCountView.OutputBoundary;
using BattleScene.UseCases.View.BuffView.OutputBoundary;
using BattleScene.UseCases.View.CharacterVibesView.OutputBoundary;
using BattleScene.UseCases.View.DestroyedPartView.OutputBoundary;
using BattleScene.UseCases.View.DigitView.OutputBoundary;
using BattleScene.UseCases.View.EnemyView.OutputBoundary;
using BattleScene.UseCases.View.FrameView.OutputBoundary;
using BattleScene.UseCases.View.GridView;
using BattleScene.UseCases.View.HitPointBarView.OutputBoundary;
using BattleScene.UseCases.View.InfoView.OutputBoundary;
using BattleScene.UseCases.View.IsContinueView.OutputBoundary;
using BattleScene.UseCases.View.MessageView.OutputBoundary;
using BattleScene.UseCases.View.PlayerImageView.OutputBoundary;
using BattleScene.UseCases.View.SelectActionView.OutputBoundary;
using BattleScene.UseCases.View.SelectSkillView.OutputBoundary;
using BattleScene.UseCases.View.TechnicalPointBarView.OutputBoundary;
using VContainer;
using VContainer.Unity;

namespace BattleScene.InterfaceAdapter
{
    public class BattleSceneLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<IEnemiesView>();
            builder.RegisterComponentInHierarchy<IInfoView>();
            builder.RegisterComponentInHierarchy<IBattleSceneInput>();
            builder.RegisterComponentInHierarchy<IGridView>();
            builder.RegisterComponentInHierarchy<IMessageView>();
            builder.RegisterComponentInHierarchy<IOrderView>();
            builder.RegisterComponentInHierarchy<IPlayerAttackCountView>();
            builder.RegisterComponentInHierarchy<IPlayerView>();
            builder.RegisterComponentInHierarchy<IPlayerStatusView>();
            builder.RegisterComponentInHierarchy<ISelectActionView>();
            builder.RegisterComponentInHierarchy<ISelectSkillView>();
            
            builder.Register<IAilmentViewPresenter, AilmentViewPresenter>(Lifetime.Singleton);
            builder.Register<IBuffViewPresenter, BuffViewPresenter>(Lifetime.Singleton);
            builder.Register<IDestroyedPartViewPresenter, DestroyedPartViewPresenter>(Lifetime.Singleton);
            builder.Register<IDigitViewPresenter, DigitViewPresenter>(Lifetime.Singleton);
            builder.Register<IEnemyViewPresenter, EnemyViewPresenter>(Lifetime.Singleton);
            builder.Register<IFrameViewPresenter, FrameViewPresenter>(Lifetime.Singleton);
            builder.Register<IHitPointBarViewPresenter, HitPointBarViewPresenter>(Lifetime.Singleton);
            builder.Register<IInfoViewPresenter, InfoViewPresenter>(Lifetime.Singleton);
            builder.Register<IIsContinueViewPresenter, IsContinueViewPresenter>(Lifetime.Singleton);
            builder.Register<IMessageViewPresenter, MessageViewPresenter>(Lifetime.Singleton);
            builder.Register<IOrderViewPresenter, OrderViewPresenter>(Lifetime.Singleton);
            builder.Register<IAttackCountViewPresenter, AttackCountViewPresenter>(Lifetime.Singleton);
            builder.Register<IPlayerImageViewPresenter, PlayerImageViewPresenter>(Lifetime.Singleton);
            builder.Register<ISelectActionViewPresenter, SelectActionViewPresenter>(Lifetime.Singleton);
            builder.Register<ISelectSkillViewPresenter, SelectSkillViewPresenter>(Lifetime.Singleton);
            builder.Register<ITechnicalPointBarViewPresenter, TechnicalPointBarViewPresenter>(Lifetime.Singleton);
            builder.Register<ICharacterVibesViewPresenter, CharacterVibesViewPresenter>(Lifetime.Singleton);
            builder.Register<IViewPresenter<GridViewOutputData>, GridViewPresenter>(Lifetime.Singleton);
            
            builder.Register<IAilmentRepository, AilmentRepository>(Lifetime.Singleton);
            builder.Register<IBodyPartRepository, BodyPartRepository>(Lifetime.Singleton);
            builder.Register<ICharacterRepository, CharacterRepository>(Lifetime.Singleton);
            builder.Register<IEnemyRepository, EnemyRepository>(Lifetime.Singleton);
            builder.Register<IResultRepository, ResultRepository>(Lifetime.Singleton);
            builder.Register<ISelectorRepository, SelectorRepository>(Lifetime.Singleton);
            builder.Register<ISkillRepository, SkillRepository>(Lifetime.Singleton);
            builder.Register<ISkillSelectorRepository, SkillSelectorRepository>(Lifetime.Singleton);
            builder.Register<ITargetRepository, TargetRepository>(Lifetime.Singleton);
            builder.Register<IRepository<ActionTimeEntity, CharacterId>, Repository<ActionTimeEntity, CharacterId>>(
                Lifetime.Singleton);
            builder.Register<IRepository<AilmentEntity, AilmentId>, Repository<AilmentEntity, AilmentId>>(
                Lifetime.Singleton);
            builder.Register<IRepository<BattleLogEntity, BattleLogId>, Repository<BattleLogEntity, BattleLogId>>(
                Lifetime.Singleton);
            builder.Register<IRepository<BodyPartEntity, BodyPartId>, Repository<BodyPartEntity, BodyPartId>>(
                Lifetime.Singleton);
            builder.Register<IRepository<BuffEntity, BuffId>, Repository<BuffEntity, BuffId>>(
                Lifetime.Singleton);
            builder.Register<IRepository<CharacterAggregate, CharacterId>, Repository<CharacterAggregate, CharacterId>>(
                Lifetime.Singleton);
            builder.Register<IRepository<EnemyEntity, CharacterId>, Repository<EnemyEntity, CharacterId>>(
                Lifetime.Singleton);
            builder.Register<IRepository<HitPointAggregate, CharacterId>, Repository<HitPointAggregate, CharacterId>>(
                Lifetime.Singleton);
            builder.Register<IRepository<OrderedItemEntity, OrderNumber>, Repository<OrderedItemEntity, OrderNumber>>(
                Lifetime.Singleton);
            builder.Register<IRepository<SkillEntity, CharacterId>, Repository<SkillEntity, CharacterId>>(
                Lifetime.Singleton);
            builder.Register<IRepository<SlipDamageEntity, SlipDamageId>, Repository<SlipDamageEntity, SlipDamageId>>(
                Lifetime.Singleton);
            builder
                .Register<IRepository<TechnicalPointEntity, CharacterId>,
                    Repository<TechnicalPointEntity, CharacterId>>(Lifetime.Singleton);
            builder.Register<IRepository<TurnEntity, TurnId>, Repository<TurnEntity, TurnId>>(Lifetime.Singleton);
            
            builder.Register<IFactory<SkillValueObject, SkillCode>, SkillFactory>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<IResource<SkillViewInfoValueObject, SkillCode>>();
            builder.Register<IFactory<PlayerPropertyValueObject, CharacterTypeCode>, PlayerPropertyFactory>
                (Lifetime.Singleton);
            builder.Register<IFactory<PropertyValueObject, CharacterTypeCode>, PropertyFactory>(Lifetime.Singleton);
            builder.Register<IAilmentFactory, AilmentFactory>(Lifetime.Singleton);
            builder.Register<IAilmentViewInfoFactory, AilmentViewInfoFactory>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<IResource<AilmentViewInfoDto, AilmentCode>>();
            builder.RegisterComponentInHierarchy<IResource<BuffViewInfoDto, BuffCode>>();
            builder.Register<IBodyPartFactory, BodyPartFactory>(Lifetime.Singleton);
            builder.Register<IResource<BodyPartViewInfoDto, BodyPartCode>, BodyPartViewInfoResource>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<IResource<EnemyViewInfoDto, CharacterTypeCode>>();
            builder.RegisterComponentInHierarchy<IResource<MessageDto, MessageCode>>();
            builder.RegisterComponentInHierarchy<IResource<PlayerViewInfoDto, CharacterTypeCode>>();
            builder.RegisterComponentInHierarchy<IResource<PropertyDto, CharacterTypeCode>>();
            builder.RegisterComponentInHierarchy<IResource<PlayerPropertyDto, CharacterTypeCode>>();

            builder.Register<ToAilmentNumberService>(Lifetime.Singleton);
            builder.Register<MessageCodeConverterService>(Lifetime.Singleton);
            
            builder.Register<AfterimageSkill>(Lifetime.Singleton);
            builder.Register<AttackSkill>(Lifetime.Singleton);
            builder.Register<BiteSkill>(Lifetime.Singleton);
            builder.Register<BiteOffSkill>(Lifetime.Singleton);
            builder.Register<ConfusionSkill>(Lifetime.Singleton);
            builder.Register<CutUpSkill>(Lifetime.Singleton);
            builder.Register<DefenceSkill>(Lifetime.Singleton);
            builder.Register<FieldRationSkill>(Lifetime.Singleton);
            builder.Register<FirstAidSkill>(Lifetime.Singleton);
            builder.Register<FlameThrowSkill>(Lifetime.Singleton);
            builder.Register<HonzougakuSkill>(Lifetime.Singleton);
            builder.Register<IshinhouSkill>(Lifetime.Singleton);
            builder.Register<KuchiyoseSkill>(Lifetime.Singleton);
            builder.Register<KyoukasuigetsuSkill>(Lifetime.Singleton);
            builder.Register<LiquidSkill>(Lifetime.Singleton);
            builder.Register<MurasameSkill>(Lifetime.Singleton);
            builder.Register<MusterStrengthSkill>(Lifetime.Singleton);
            builder.Register<NadegiriSkill>(Lifetime.Singleton);
            builder.Register<NumbLiquidSkill>(Lifetime.Singleton);
            builder.Register<OnikoroshiSkill>(Lifetime.Singleton);
            builder.Register<PunchSkill>(Lifetime.Singleton);
            builder.Register<PutScytheSkill>(Lifetime.Singleton);
            builder.Register<RaikiriSkill>(Lifetime.Singleton);
            builder.Register<RandomShotsSkill>(Lifetime.Singleton);
            builder.Register<ShichishitouSkill>(Lifetime.Singleton);
            builder.Register<SilverBulletSkill>(Lifetime.Singleton);
            builder.Register<SmokeBombSkill>(Lifetime.Singleton);
            builder.Register<StarShellSkill>(Lifetime.Singleton);
            builder.Register<StringerSkill>(Lifetime.Singleton);
            builder.Register<TaserGunSkill>(Lifetime.Singleton);
            builder.Register<UtsusemiSkill>(Lifetime.Singleton);
            builder.Register<WabisukeSkill>(Lifetime.Singleton);

            builder.Register<AbsoluteConfusion>(Lifetime.Singleton);
            builder.Register<AfterImage>(Lifetime.Singleton);
            builder.Register<AlwaysHitDamage>(Lifetime.Singleton);
            builder.Register<BasicCure>(Lifetime.Singleton);
            builder.Register<BasicDamage>(Lifetime.Singleton);
            builder.Register<BasicRestoreTechnicalPointSkillElement>(Lifetime.Singleton);
            builder.Register<BleedingSkill>(Lifetime.Singleton);
            builder.Register<Blind>(Lifetime.Singleton);
            builder.Register<BurningReset>(Lifetime.Singleton);
            builder.Register<BurningSkill>(Lifetime.Singleton);
            builder.Register<Confusion>(Lifetime.Singleton);
            builder.Register<ConstantDamage>(Lifetime.Singleton);
            builder.Register<Defence>(Lifetime.Singleton);
            builder.Register<DestroyArm>(Lifetime.Singleton);
            builder.Register<destroyLeg>(Lifetime.Singleton);
            builder.Register<DestroyStomach>(Lifetime.Singleton);
            builder.Register<EnemyParalysis>(Lifetime.Singleton);
            builder.Register<FirstAid>(Lifetime.Singleton);
            builder.Register<FiveTimeDamage>(Lifetime.Singleton);
            builder.Register<Honzougaku>(Lifetime.Singleton);
            builder.Register<Ishinhou>(Lifetime.Singleton);
            builder.Register<LightningDamage>(Lifetime.Singleton);
            builder.Register<MusterStrength>(Lifetime.Singleton);
            builder.Register<Nadegiri>(Lifetime.Singleton);
            builder.Register<Paralysis>(Lifetime.Singleton);
            builder.Register<PoisoningSkill>(Lifetime.Singleton);
            builder.Register<RandomShot>(Lifetime.Singleton);
            builder.Register<Shichishitou>(Lifetime.Singleton);
            builder.Register<StarShell>(Lifetime.Singleton);
            builder.Register<SuffocationSkill>(Lifetime.Singleton);
            builder.Register<Utsusemi>(Lifetime.Singleton);
            builder.Register<Wabisuke>(Lifetime.Singleton);
            
            builder.Register<InitializeBattleState>(Lifetime.Singleton);
            builder.Register<InitializePlayerState>(Lifetime.Singleton);
            builder.Register<InitializeEnemyState>(Lifetime.Singleton);
            builder.Register<OrderState>(Lifetime.Singleton);
            builder.Register<PlayerSelectActionState>(Lifetime.Singleton);
            builder.Register<SelectTargetStateFactory>(Lifetime.Singleton);
            builder.Register<EnemySelectSkillState>(Lifetime.Singleton);
            builder.Register<SkillStateFactory>(Lifetime.Singleton);
            builder.Register<BuffStateFactory>(Lifetime.Singleton);
            builder.Register<BuffMessageState>(Lifetime.Singleton);
            builder.Register<DamageStateFactory>(Lifetime.Singleton);
            builder.Register<RestoreStateFactory>(Lifetime.Singleton);
            builder.Register<SkillMessageStateFactory>(Lifetime.Singleton);
            builder.Register<SkillEndState>(Lifetime.Singleton);
            builder.Register<TurnEndState>(Lifetime.Singleton);
            
            builder.Register<ActionTimeCreatorService>(Lifetime.Singleton);
            builder.Register<AgilityToSpeedService>(Lifetime.Singleton);
            builder.Register<AilmentSkillService>(Lifetime.Singleton);
            builder.Register<AttackCounterService>(Lifetime.Singleton);
            builder.Register<BuffGeneratorService>(Lifetime.Singleton);
            builder.Register<CharacterOutputDataCreatorService>(Lifetime.Singleton);
            builder.Register<CureSkillService>(Lifetime.Singleton);
            builder.Register<DamageGeneratorService>(Lifetime.Singleton);
            builder.Register<DestroyedPartGeneratorService>(Lifetime.Singleton);
            builder.Register<HitPointCreatorService>(Lifetime.Singleton);
            builder.Register<OrderedItemCreatorService>(Lifetime.Singleton);
            builder.Register<ResetSkillService>(Lifetime.Singleton);
            builder.Register<RestoreGeneratorService>(Lifetime.Singleton);
            builder.Register<SelectSkillService>(Lifetime.Singleton);
            builder.Register<SkillCreatorService>(Lifetime.Singleton);
            builder.Register<SkillService>(Lifetime.Singleton);
            builder.Register<SlipDamageService>(Lifetime.Singleton);
            builder.Register<ToBodyPartNumberService>(Lifetime.Singleton);
            builder.Register<ToBuffNumberService>(Lifetime.Singleton);

            builder.Register<AilmentDomainService>(Lifetime.Singleton);
            builder.Register<BattleLoggerService>(Lifetime.Singleton);
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
            
            builder.Register<DamageEvaluatorService>(Lifetime.Singleton);
            builder.Register<IsHitEvaluatorService>(Lifetime.Singleton);
            builder.Register<AttacksWeakPointEvaluatorService>(Lifetime.Singleton);
            
            builder.Register<OrderView>(Lifetime.Singleton);

            builder.RegisterEntryPoint<StateMachine>();

            builder.Register<OrderDecision>(Lifetime.Singleton);

            builder.Register<EnemySkillSelector>(Lifetime.Singleton);
        }
    }
}