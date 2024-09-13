using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
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
using BattleScene.InterfaceAdapter.Interface;
using BattleScene.InterfaceAdapter.Presenter;
using BattleScene.InterfaceAdapter.Presenter.AilmentsView;
using BattleScene.InterfaceAdapter.Presenter.BuffView;
using BattleScene.InterfaceAdapter.Presenter.CharacterVibesView;
using BattleScene.InterfaceAdapter.Presenter.DestroyedPartView;
using BattleScene.InterfaceAdapter.Presenter.DigitView;
using BattleScene.InterfaceAdapter.Presenter.Dto;
using BattleScene.InterfaceAdapter.Presenter.EnemyView;
using BattleScene.InterfaceAdapter.Presenter.FrameView;
using BattleScene.InterfaceAdapter.Presenter.InfoView;
using BattleScene.InterfaceAdapter.Presenter.MessageView;
using BattleScene.InterfaceAdapter.Presenter.OrderView;
using BattleScene.InterfaceAdapter.Presenter.PlayerAttackCountView;
using BattleScene.InterfaceAdapter.Presenter.PlayerView;
using BattleScene.InterfaceAdapter.Presenter.SelectSkillView;
using BattleScene.InterfaceAdapter.Presenter.StatusBarView;
using BattleScene.InterfaceAdapter.Presenter.ViewPresenter;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.InterfaceAdapter.Skill;
using BattleScene.InterfaceAdapter.Skill.SkillElement;
using BattleScene.InterfaceAdapter.State.Battle;
using BattleScene.InterfaceAdapter.State.Skill;
using BattleScene.UseCases.Interface;
using BattleScene.UseCases.IPresenter;
using BattleScene.UseCases.IService;
using BattleScene.UseCases.Output;
using BattleScene.UseCases.OutputData;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.UseCase;
using BattleScene.UseCases.View.AilmentView.OutputBoundary;
using BattleScene.UseCases.View.AttackCountView.OutputBoundary;
using BattleScene.UseCases.View.BuffView.OutputBoundary;
using BattleScene.UseCases.View.CharacterVibesView.OutputBoundary;
using BattleScene.UseCases.View.DestroyedPartView.OutputBoundary;
using BattleScene.UseCases.View.DigitView.OutputBoundary;
using BattleScene.UseCases.View.DigitView.OutputDataFactory;
using BattleScene.UseCases.View.EnemyView.OutputBoundary;
using BattleScene.UseCases.View.FrameView.OutputBoundary;
using BattleScene.UseCases.View.HitPointBarView.OutputBoundary;
using BattleScene.UseCases.View.HitPointBarView.OutputDataFactory;
using BattleScene.UseCases.View.InfoView.OutputBoundary;
using BattleScene.UseCases.View.MessageView.OutputBoundary;
using BattleScene.UseCases.View.PlayerImageView.OutputBoundary;
using BattleScene.UseCases.View.SelectSkillView.OutputBoundary;
using BattleScene.UseCases.View.TechnicalPointBarView.OutputBoundary;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using BuffViewDto = BattleScene.InterfaceAdapter.DataAccess.Dto.BuffViewDto;
using EnemyViewDto = BattleScene.InterfaceAdapter.DataAccess.Dto.EnemyViewDto;
using PlayerViewDto = BattleScene.InterfaceAdapter.DataAccess.Dto.PlayerViewDto;

#if UNITY_EDITOR
using BattleScene.UseCases.Service.DebugService;
#endif

namespace BattleScene.InterfaceAdapter
{
    public class BattleSceneLifetimeScope : LifetimeScope
    {
        
#if UNITY_EDITOR
        [SerializeField] private bool debugMode;
#else
        private bool debugMode = false;
#endif
        
        protected override void Configure(IContainerBuilder builder)
        {
            if (debugMode)
            {
                // デバッグモード時に注入するインスタンスを登録する
                builder.Register<IEnemiesRegistererService, SameEnemiesRegistererService>(Lifetime.Singleton);
                builder.Register<IEnemySkillSelectorService, EnemySlipSelectorService>(Lifetime.Singleton);
            }
            else
            {
                #region RegisterService
                builder.Register<IEnemiesRegistererService, EnemiesRegistererService>(Lifetime.Singleton);
                builder.Register<IEnemySkillSelectorService, EnemySkillSelectorService>(Lifetime.Singleton);
                #endregion
            }
            
            #region RegisterView
            builder.RegisterComponentInHierarchy<IEnemiesView>();
            builder.RegisterComponentInHierarchy<IInfoView>();
            builder.RegisterComponentInHierarchy<IBattleSceneInput>();
            builder.RegisterComponentInHierarchy<IGridView>();
            builder.RegisterComponentInHierarchy<IMessageView>();
            builder.RegisterComponentInHierarchy<IOrderView>();
            builder.RegisterComponentInHierarchy<IPlayerAttackCountView>();
            builder.RegisterComponentInHierarchy<IPlayerView>();
            builder.RegisterComponentInHierarchy<IPlayerStatusView>();
            builder.RegisterComponentInHierarchy<ISelectSkillView>();
            builder.RegisterComponentInHierarchy<ITargetView>();
            builder.RegisterComponentInHierarchy<IVIew<SkillViewDto>>();
            #endregion

            #region RegisterPresenter
            builder.Register<IAilmentViewPresenter, AilmentViewPresenter>(Lifetime.Singleton);
            builder.Register<IBuffViewPresenter, BuffViewPresenter>(Lifetime.Singleton);
            builder.Register<IDestroyedPartViewPresenter, DestroyedPartViewPresenter>(Lifetime.Singleton);
            builder.Register<IDigitViewPresenter, DigitViewPresenter>(Lifetime.Singleton);
            builder.Register<IEnemyViewPresenter, EnemyViewPresenter>(Lifetime.Singleton);
            builder.Register<IFrameViewPresenter, FrameViewPresenter>(Lifetime.Singleton);
            builder.Register<IHitPointBarViewPresenter, HitPointBarViewPresenter>(Lifetime.Singleton);
            builder.Register<IInfoViewPresenter, InfoViewPresenter>(Lifetime.Singleton);
            builder.Register<IMessageViewPresenter, MessageViewPresenter>(Lifetime.Singleton);
            builder.Register<IOrderViewPresenter, OrderViewPresenter>(Lifetime.Singleton);
            builder.Register<IAttackCountViewPresenter, AttackCountViewPresenter>(Lifetime.Singleton);
            builder.Register<IPlayerImageViewPresenter, PlayerImageViewPresenter>(Lifetime.Singleton);
            builder.Register<ISelectSkillViewPresenter, SelectSkillViewPresenter>(Lifetime.Singleton);
            builder.Register<ITechnicalPointBarViewPresenter, TechnicalPointBarViewPresenter>(Lifetime.Singleton);
            builder.Register<ICharacterVibesViewPresenter, CharacterVibesViewPresenter>(Lifetime.Singleton);
            builder.Register<IViewPresenter<GridViewOutputData>, GridViewPresenter>(Lifetime.Singleton);
            builder.Register<ITargetViewPresenter, TargetViewPresenter>(Lifetime.Singleton);
            builder.Register<ISkillViewPresenter, SkillViewPresenter>(Lifetime.Singleton);
            #endregion

            #region RegisterResource
            builder.RegisterComponentInHierarchy<IResource<AilmentPropertyDto, AilmentCode>>();
            builder.RegisterComponentInHierarchy<IAilmentViewResource>();
            builder.RegisterComponentInHierarchy<IResource<BattlePropertyDto>>();
            builder.Register<IResource<BodyPartViewDto, BodyPartCode>, BodyPartViewResource>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<IResource<BuffViewDto, BuffCode>>();
            builder.RegisterComponentInHierarchy<IResource<CharacterPropertyDto, CharacterTypeCode>>();
            builder.RegisterComponentInHierarchy<IResource<EnemyViewDto, CharacterTypeCode>>();
            builder.RegisterComponentInHierarchy<IResource<MessageDto, MessageCode>>();
            builder.RegisterComponentInHierarchy<IResource<PlayerViewDto, CharacterTypeCode>>();
            builder.RegisterComponentInHierarchy<IResource<PlayerPropertyDto, CharacterTypeCode>>();
            builder.RegisterComponentInHierarchy<IResource<PlayerImagePathDto, PlayerImageCode>>();
            builder.RegisterComponentInHierarchy<IResource<SkillPropertyDto, SkillCode>>();
            #endregion
            
            #region RegisterSkill
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
            #endregion

            #region RegisterSkillElement
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
            builder.Register<DestroyLeg>(Lifetime.Singleton);
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
            #endregion
            
            #region RegisterFactory
            builder.Register<IFactory<AilmentPropertyValueObject, AilmentCode>, AilmentPropertyFactory>(
                Lifetime.Singleton);
            builder.Register<IFactory<BattlePropertyValueObject>, BattlePropertyFactory>(Lifetime.Singleton);
            builder.Register<IFactory<PropertyValueObject, CharacterTypeCode>, PropertyFactory>(Lifetime.Singleton);
            builder.Register<IFactory<PlayerPropertyValueObject, CharacterTypeCode>, PlayerPropertyFactory>
                (Lifetime.Singleton);
            builder.Register<IFactory<SkillValueObject, SkillCode>, SkillFactory>(Lifetime.Singleton);
            #endregion
            
            #region RegisterRepository
            builder.Register<IAilmentRepository, AilmentRepository>(Lifetime.Singleton);
            builder.Register<IBodyPartRepository, BodyPartRepository>(Lifetime.Singleton);
            builder.Register<ICharacterRepository, CharacterRepository>(Lifetime.Singleton);
            builder.Register<IEnemyRepository, EnemyRepository>(Lifetime.Singleton);
            builder.Register<IRepository<AilmentEntity, (CharacterId, AilmentCode)>, Repository<AilmentEntity, (CharacterId, AilmentCode)>>(
                Lifetime.Singleton);
            builder.Register<IRepository<BattleLogEntity, BattleLogId>, Repository<BattleLogEntity, BattleLogId>>(
                Lifetime.Singleton);
            builder.Register<IRepository<BodyPartEntity, (CharacterId, BodyPartCode)>, Repository<BodyPartEntity, (CharacterId, BodyPartCode)>>(
                Lifetime.Singleton);
            builder.Register<IRepository<BuffEntity, (CharacterId, BuffCode)>, Repository<BuffEntity, (CharacterId, BuffCode)>>(
                Lifetime.Singleton);
            builder.Register<IRepository<CharacterEntity, CharacterId>, Repository<CharacterEntity, CharacterId>>(
                Lifetime.Singleton);
            builder.Register<IRepository<EnemyEntity, CharacterId>, Repository<EnemyEntity, CharacterId>>(
                Lifetime.Singleton);
            builder.Register<IRepository<OrderedItemEntity, OrderId>, Repository<OrderedItemEntity, OrderId>>(
                Lifetime.Singleton);
            builder.Register<IRepository<SlipDamageEntity, SlipDamageCode>, Repository<SlipDamageEntity, SlipDamageCode>>(
                Lifetime.Singleton);
            builder.Register<IRepository<TurnEntity, TurnId>, Repository<TurnEntity, TurnId>>(Lifetime.Singleton);
            #endregion

            #region RegisterInterfaceAdapterService
            builder.Register<ToAilmentNumberService>(Lifetime.Singleton);
            builder.Register<MessageCodeConverterService>(Lifetime.Singleton);
            #endregion

            #region RegisterState
            builder.Register<InitializeBattleState>(Lifetime.Singleton);
            builder.Register<InitializePlayerState>(Lifetime.Singleton);
            builder.Register<InitializeEnemyState>(Lifetime.Singleton);
            builder.Register<OrderState>(Lifetime.Singleton);
            builder.Register<PlayerSelectActionState>(Lifetime.Singleton);
            builder.Register<PlayerSelectSkillState>(Lifetime.Singleton);
            builder.Register<SelectTargetStateFactory>(Lifetime.Singleton);
            builder.Register<EnemySelectSkillState>(Lifetime.Singleton);
            builder.Register<SkillStateFactory>(Lifetime.Singleton);
            builder.Register<AilmentStateFactory>(Lifetime.Singleton);
            builder.Register<AilmentMessageState>(Lifetime.Singleton);
            builder.Register<AilmentFailureState>(Lifetime.Singleton);
            builder.Register<BuffStateFactory>(Lifetime.Singleton);
            builder.Register<BuffMessageState>(Lifetime.Singleton);
            builder.Register<DamageStateFactory>(Lifetime.Singleton);
            builder.Register<DamageMessageState>(Lifetime.Singleton);
            builder.Register<RestoreStateFactory>(Lifetime.Singleton);
            builder.Register<RestoreMessageState>(Lifetime.Singleton);
            builder.Register<SlipStateFactory>(Lifetime.Singleton);
            builder.Register<SlipMessageState>(Lifetime.Singleton);
            builder.Register<SlipFailureState>(Lifetime.Singleton);
            builder.Register<SkillMessageStateFactory>(Lifetime.Singleton);
            builder.Register<SkillEndState>(Lifetime.Singleton);
            builder.Register<SkillQuitState>(Lifetime.Singleton);
            builder.Register<TurnEndState>(Lifetime.Singleton);
            #endregion

            #region RegisterService
            builder.Register<ActionTimeService>(Lifetime.Singleton);
            builder.Register<ActualTargetIdPickerService>(Lifetime.Singleton);
            builder.Register<AilmentGeneratorService>(Lifetime.Singleton);
            builder.Register<AilmentRegistererService>(Lifetime.Singleton);
            builder.Register<AttackCounterService>(Lifetime.Singleton);
            builder.Register<AttacksWeakPointEvaluatorService>(Lifetime.Singleton);
            builder.Register<BuffGeneratorService>(Lifetime.Singleton);
            builder.Register<CharacterOutputDataCreatorService>(Lifetime.Singleton);
            builder.Register<CharacterPropertyFactoryService>(Lifetime.Singleton);
            builder.Register<DamageEvaluatorService>(Lifetime.Singleton);
            builder.Register<DamageGeneratorService>(Lifetime.Singleton);
            builder.Register<DamageRegistererService>(Lifetime.Singleton);
            builder.Register<DestroyedPartGeneratorService>(Lifetime.Singleton);
            builder.Register<IsHitEvaluatorService>(Lifetime.Singleton);
            builder.Register<OrderService>(Lifetime.Singleton);
            builder.Register<RestoreGeneratorService>(Lifetime.Singleton);
            builder.Register<SlipDamageGeneratorService>(Lifetime.Singleton);
            builder.Register<SlipGeneratorService>(Lifetime.Singleton);
            builder.Register<SlipRegistererService>(Lifetime.Singleton);
            builder.Register<ToBodyPartNumberService>(Lifetime.Singleton);
            builder.Register<ToBuffNumberService>(Lifetime.Singleton);
            #endregion

            #region RegisterDomainService
            builder.Register<AilmentDomainService>(Lifetime.Singleton);
            builder.Register<BattleLogDomainService>(Lifetime.Singleton);
            builder.Register<BattleLoggerService>(Lifetime.Singleton);
            builder.Register<BodyPartDomainService>(Lifetime.Singleton);
            builder.Register<BuffDomainService>(Lifetime.Singleton);
            builder.Register<CharactersDomainService>(Lifetime.Singleton);
            builder.Register<EnemiesDomainService>(Lifetime.Singleton);
            builder.Register<OrderedItemsDomainService>(Lifetime.Singleton);
            builder.Register<PlayerDomainService>(Lifetime.Singleton);
            builder.Register<SlipDomainService>(Lifetime.Singleton);
            builder.Register<TargetDomainService>(Lifetime.Singleton);
            #endregion

            #region RegisterEntryPoint
            builder.RegisterEntryPoint<StateMachine>();
            #endregion
            
            builder.Register<OrderDecision>(Lifetime.Singleton);
            builder.Register<OrderView>(Lifetime.Singleton);

            builder.Register<DamageDigitOutputDataFactory>(Lifetime.Singleton);
            builder.Register<HitPointBarOutputDataFactory>(Lifetime.Singleton);
            builder.Register<ActionDescriptionMessageOutput>(Lifetime.Singleton);
        }
    }
}