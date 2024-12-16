using BattleScene.DataAccesses;
using BattleScene.DataAccesses.Dto;
using BattleScene.DataAccesses.Factory;
using BattleScene.DataAccesses.Repository;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.DomainServices;
using BattleScene.Domain.Entities;
using BattleScene.Domain.Ids;
using BattleScene.Domain.ValueObjects;
using BattleScene.Presenters;
using BattleScene.Presenters.PresenterFacades;
using BattleScene.Presenters.Presenters;
using BattleScene.Presenters.ReactivePresenters;
using BattleScene.Presenters.Services;
using BattleScene.Presenters.Services.Replacements;
using BattleScene.Presenters.Services.Replacements.Interfaces;
using BattleScene.Presenters.SkillComponents;
using BattleScene.Presenters.Skills;
using BattleScene.Presenters.StateMachines;
using BattleScene.Presenters.States.Battle;
using BattleScene.Presenters.States.Skill;
using BattleScene.Presenters.States.Turn;
using BattleScene.UseCases.IServices;
using BattleScene.UseCases.Services;
using BattleScene.UseCases.Services.Order;
using BattleScene.UseCases.UseCases;
using BattleScene.Views;
using BattleScene.Views.Inputs;
using BattleScene.Views.IServices;
using BattleScene.Views.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using static VContainer.Lifetime;

namespace BattleScene
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
                builder.RegisterComponentInHierarchy<IEnemySelectorService>();
                builder.RegisterComponentInHierarchy<IEnemySkillSelectorService>();
                builder.RegisterComponentInHierarchy<IMyRandomService>();
                builder.Register<MyRandomService>(Singleton);
            }
            else
            {
                #region RegisterService

                builder.Register<IEnemySelectorService, EnemySelectorService>(Singleton);
                builder.Register<IEnemySkillSelectorService, EnemySkillSelectorService>(Singleton);
                builder.Register<IMyRandomService, MyRandomService>(Singleton);

                #endregion
            }

            #region RegisterView

            builder.RegisterComponentInHierarchy<EnemyGroupView>();
            builder.RegisterComponentInHierarchy<InfoView>();
            builder.RegisterComponentInHierarchy<TableView>();
            builder.RegisterComponentInHierarchy<MessageView>();
            builder.RegisterComponentInHierarchy<OrderView>();
            builder.RegisterComponentInHierarchy<PlayerAttackCountView>();
            builder.RegisterComponentInHierarchy<PlayerView>();
            builder.RegisterComponentInHierarchy<PlayerStatusView>();
            builder.RegisterComponentInHierarchy<TargetView>();

            #endregion

            builder.RegisterComponentInHierarchy<EntryPoint>();
            builder.RegisterComponentInHierarchy<BattleSceneInput>();

            #region RegisterPresenter

            builder.Register<AttackCountViewPresenter>(Singleton);
            builder.Register<CureViewPresenter>(Singleton);
            builder.Register<DamageViewPresenter>(Singleton);
            builder.Register<EnemyImageViewPresenter>(Singleton);
            builder.Register<TableViewPresenter>(Singleton);
            builder.Register<InfoViewPresenter>(Singleton);
            builder.Register<MessageViewPresenter>(Singleton);
            builder.Register<OrderViewPresenter>(Singleton);
            builder.Register<PlayerImageViewPresenter>(Singleton);
            builder.Register<RestoreViewPresenter>(Singleton);
            builder.Register<SkillViewPresenter>(Singleton);
            builder.Register<TargetViewPresenter>(Singleton);
            builder.Register<VibrationViewPresenter>(Singleton);

            #endregion

            #region RegisterReactivePresenter

            builder.Register<IReactive<AilmentEntity>, AilmentViewReactivePresenter>(Singleton);
            builder.Register<IReactive<BodyPartEntity>, BodyPartViewReactivePresenter>(Singleton);
            builder.Register<IReactive<BuffEntity>, BuffViewReactivePresenter>(Singleton);
            builder.Register<IReactive<CharacterEntity>, HitPointViewReactivePresenter>(Singleton);
            builder.Register<IReactive<CharacterEntity>, TechnicalPointViewReactivePresenter>(Singleton);
            builder.Register<IReactive<SlipEntity>, SlipViewReactivePresenter>(Singleton);

            #endregion

            #region RegisterFacade

            builder.Register<AilmentOutputPresenterFacade>(Singleton);
            builder.Register<BuffOutputPresenterFacade>(Singleton);
            builder.Register<CharacterDeadPresenterFacade>(Singleton);
            builder.Register<CureOutputPresenterFacade>(Singleton);
            builder.Register<EnhanceOutputPresenterFacade>(Singleton);
            builder.Register<InitializeEnemyPresenterFacade>(Singleton);
            builder.Register<ResetAilmentPresenterFacade>(Singleton);
            builder.Register<DamageOutputPresenterFacade>(Singleton);
            builder.Register<DestroyOutputPresenterFacade>(Singleton);
            builder.Register<PlayerLosePresenterFacade>(Singleton);
            builder.Register<PlayerSelectActionPresenterFacade>(Singleton);
            builder.Register<PlayerSelectSkillPresenterFacade>(Singleton);
            builder.Register<PlayerSelectTargetPresenterFacade>(Singleton);
            builder.Register<PlayerWinPresenterFacade>(Singleton);
            builder.Register<ResetOutputPresenterFacade>(Singleton);
            builder.Register<RestoreOutputPresenterFacade>(Singleton);
            builder.Register<SkillPresenterFacade>(Singleton);
            builder.Register<SlipDamagePresenterFacade>(Singleton);
            builder.Register<SlipOutputPresenterFacade>(Singleton);

            #endregion

            #region RegisterResource

            builder.RegisterComponentInHierarchy<IResource<AilmentPropertyDto, AilmentCode>>();
            builder.RegisterComponentInHierarchy<IResource<AilmentViewDto, AilmentCode, SlipCode>>();
            builder.RegisterComponentInHierarchy<IResource<BattlePropertyDto>>();
            builder.RegisterComponentInHierarchy<IResource<BodyPartPropertyDto, BodyPartCode>>();
            builder.RegisterComponentInHierarchy<IResource<BodyPartViewDto, BodyPartCode>>();
            builder.RegisterComponentInHierarchy<IResource<BuffViewDto, BuffCode>>();
            builder.RegisterComponentInHierarchy<IResource<CharacterPropertyDto, CharacterTypeCode>>();
            builder.RegisterComponentInHierarchy<IResource<EnemyViewDto, CharacterTypeCode>>();
            builder.RegisterComponentInHierarchy<IResource<EnhanceViewDto, EnhanceCode>>();
            builder.RegisterComponentInHierarchy<IResource<MessageDto, MessageCode>>();
            builder.RegisterComponentInHierarchy<IResource<PlayerViewDto, CharacterTypeCode>>();
            builder.RegisterComponentInHierarchy<IResource<PlayerPropertyDto, CharacterTypeCode>>();
            builder.RegisterComponentInHierarchy<IResource<PlayerImageDto, PlayerImageCode>>();
            builder.RegisterComponentInHierarchy<IResource<SkillViewDto, SkillCode>>();

            #endregion

            #region RegisterSkill

            builder.Register<AfterimageSkill>(Transient);
            builder.Register<AttackSkill>(Transient);
            builder.Register<BiteSkill>(Transient);
            builder.Register<BiteOffSkill>(Transient);
            builder.Register<ConfusionSkill>(Transient);
            builder.Register<CutUpSkill>(Transient);
            builder.Register<DefenceSkill>(Transient);
            builder.Register<FieldRationSkill>(Transient);
            builder.Register<FirstAidSkill>(Transient);
            builder.Register<FlameThrowSkill>(Transient);
            builder.Register<HonzougakuSkill>(Transient);
            builder.Register<IshinhouSkill>(Transient);
            builder.Register<KuchiyoseSkill>(Transient);
            builder.Register<KyoukasuigetsuSkill>(Transient);
            builder.Register<LiquidSkill>(Transient);
            builder.Register<MetronomeSkill>(Transient);
            builder.Register<MurasameSkill>(Transient);
            builder.Register<MusterStrengthSkill>(Transient);
            builder.Register<NadegiriSkill>(Transient);
            builder.Register<NumbLiquidSkill>(Transient);
            builder.Register<OnikoroshiSkill>(Transient);
            builder.Register<ParalysisSkill>(Transient);
            builder.Register<PoisoningSkill>(Transient);
            builder.Register<PunchSkill>(Transient);
            builder.Register<PutScytheSkill>(Transient);
            builder.Register<RaikiriSkill>(Transient);
            builder.Register<RandomShotsSkill>(Transient);
            builder.Register<ShichishitouSkill>(Transient);
            builder.Register<SilverBulletSkill>(Transient);
            builder.Register<SmokeBombSkill>(Transient);
            builder.Register<StarShellSkill>(Transient);
            builder.Register<StringerSkill>(Transient);
            builder.Register<SuffocationSkill>(Transient);
            builder.Register<TaserGunSkill>(Transient);
            builder.Register<UtsusemiSkill>(Transient);
            builder.Register<WabisukeSkill>(Transient);

            #endregion

            #region RegisterSkillComponent

            builder.Register<AbsoluteConfusion>(Singleton);
            builder.Register<AfterImage>(Singleton);
            builder.Register<AlwaysHitDamage>(Singleton);
            builder.Register<BasicCure>(Singleton);
            builder.Register<BasicDamage>(Singleton);
            builder.Register<BleedingSkill>(Singleton);
            builder.Register<Blind>(Singleton);
            builder.Register<BurningRecovery>(Singleton);
            builder.Register<BurningSkill>(Singleton);
            builder.Register<Confusion>(Singleton);
            builder.Register<ConstantDamage>(Singleton);
            builder.Register<Defence>(Singleton);
            builder.Register<DestroyArm>(Singleton);
            builder.Register<DestroyLeg>(Singleton);
            builder.Register<DestroyStomach>(Singleton);
            builder.Register<EnemyParalysis>(Singleton);
            builder.Register<FirstAid>(Singleton);
            builder.Register<FiveTimeDamage>(Singleton);
            builder.Register<Honzougaku>(Singleton);
            builder.Register<Ishinhou>(Singleton);
            builder.Register<LightningDamage>(Singleton);
            builder.Register<MusterStrength>(Singleton);
            builder.Register<Nadegiri>(Singleton);
            builder.Register<Paralysis>(Singleton);
            builder.Register<PoisoningSkill>(Singleton);
            builder.Register<RandomShot>(Singleton);
            builder.Register<Shichishitou>(Singleton);
            builder.Register<StarShell>(Singleton);
            builder.Register<SuffocationSkill>(Singleton);
            builder.Register<Utsusemi>(Singleton);
            builder.Register<Wabisuke>(Singleton);

            #endregion

            builder.Register<SkillServiceLocator>(Singleton);

            #region RegisterFactory

            builder.Register<IFactory<AilmentPropertyValueObject, AilmentCode>, AilmentPropertyFactory>(Singleton);
            builder.Register<IFactory<BattlePropertyValueObject>, BattlePropertyFactory>(Singleton);
            builder.Register<IFactory<BodyPartPropertyValueObject, BodyPartCode>, BodyPartPropertyFactory>(Singleton);
            builder.Register<IFactory<CharacterPropertyValueObject, CharacterTypeCode>, CharacterPropertyFactory>(
                Singleton);
            builder.Register<IFactory<PlayerPropertyValueObject, CharacterTypeCode>, PlayerPropertyFactory>(Singleton);
            builder.Register<IFactory<SkillValueObject, SkillCode>, SkillFactory>(Singleton);

            #endregion

            #region RegisterCollection

            builder.Register<IRepository<AilmentEntity, (CharacterId, AilmentCode)>,
                Repository<AilmentEntity, (CharacterId, AilmentCode)>>(Singleton);
            builder
                .Register<IRepository<BattleEventEntity, BattleEventId>, Repository<BattleEventEntity, BattleEventId>>(
                    Singleton);
            builder.Register<IRepository<BodyPartEntity, (CharacterId, BodyPartCode)>,
                Repository<BodyPartEntity, (CharacterId, BodyPartCode)>>(Singleton);
            builder.Register<IRepository<BuffEntity, (CharacterId, BuffCode)>,
                Repository<BuffEntity, (CharacterId, BuffCode)>>(Singleton);
            builder.Register<IRepository<CharacterEntity, CharacterId>, Repository<CharacterEntity, CharacterId>>(
                Singleton);
            builder.Register<IRepository<EnhanceEntity, (CharacterId, EnhanceCode)>,
                Repository<EnhanceEntity, (CharacterId, EnhanceCode)>>(Singleton);
            builder.Register<IRepository<OrderItemEntity, OrderItemId>, Repository<OrderItemEntity, OrderItemId>>(
                Singleton);
            builder.Register<IRepository<SlipEntity, SlipCode>, Repository<SlipEntity, SlipCode>>(Singleton);
            builder.Register<IRepository<TurnEntity, TurnId>, Repository<TurnEntity, TurnId>>(Singleton);

            #endregion

            #region RegisterInterfaceAdapterService

            builder.Register<ActorService>(Singleton);
            builder.Register<IMyTextMeshProService, MyTextMeshProService>(Singleton);
            builder.Register<IReplacementService, ActorReplacementService>(Singleton);
            builder.Register<IReplacementService, AilmentReplacementService>(Singleton);
            builder.Register<IReplacementService, BuffReplacementService>(Singleton);
            builder.Register<IReplacementService, CureReplacementService>(Singleton);
            builder.Register<IReplacementService, DamageReplacementService>(Singleton);
            builder.Register<IReplacementService, DeadReplacementService>(Singleton);
            builder.Register<IReplacementService, DestroyedReplacementService>(Singleton);
            builder.Register<IReplacementService, PlayerReplacementService>(Singleton);
            builder.Register<IReplacementService, SkillReplacementService>(Singleton);
            builder.Register<IReplacementService, SlipReplacementService>(Singleton);
            builder.Register<IReplacementService, TargetReplacementService>(Singleton);
            builder.Register<IReplacementService, TechnicalPointReplacementService>(Singleton);
            builder.Register<ReplacementCommonService>(Singleton);
            builder.Register<ToIndexService>(Singleton);

            #endregion

            builder.Register<TurnStateMachine>(Singleton);
            builder.Register<SkillStateMachine>(Singleton);

            #region RegisterState

            // BattleState
            builder.Register<InitializeBattleState>(Singleton);
            builder.Register<InitializePlayerState>(Singleton);
            builder.Register<InitializeEnemyState>(Singleton);
            builder.Register<TurnState>(Singleton);
            builder.Register<BattleStopState>(Singleton);
            // TurnState
            builder.Register<TurnStartState>(Singleton);
            builder.Register<PlayerSelectActionState>(Singleton);
            builder.Register<PlayerSelectSkillState>(Singleton);
            builder.Register<PlayerSelectTargetState>(Singleton);
            builder.Register<EnemySelectActionState>(Singleton);
            builder.Register<CantActionState>(Singleton);
            builder.Register<SlipDamageState>(Singleton);
            builder.Register<ResetAilmentState>(Singleton);
            builder.Register<SkillState>(Singleton);
            builder.Register<CharacterDeadState>(Singleton);
            builder.Register<AdvanceTurnState>(Singleton);
            builder.Register<PlayerLoseState>(Singleton);
            builder.Register<PlayerWinState>(Singleton);
            builder.Register<TurnStopState>(Singleton);
            // SkillState
            builder.Register<SkillStartState<AilmentValueObject>>(Singleton);
            builder.Register<SkillStartState<DestroyValueObject>>(Singleton);
            builder.Register<SkillStartState<DamageValueObject>>(Singleton);
            builder.Register<SkillStartState<BuffValueObject>>(Singleton);
            builder.Register<SkillStartState<CureValueObject>>(Singleton);
            builder.Register<SkillStartState<EnhanceValueObject>>(Singleton);
            builder.Register<SkillStartState<RecoveryValueObject>>(Singleton);
            builder.Register<SkillStartState<RestoreValueObject>>(Singleton);
            builder.Register<SkillStartState<SlipValueObject>>(Singleton);
            builder.Register<SkillOutputState<AilmentValueObject>, AilmentOutputState>(Singleton);
            builder.Register<SkillOutputState<BuffValueObject>, BuffOutputState>(Singleton);
            builder.Register<SkillOutputState<CureValueObject>, CureOutputState>(Singleton);
            builder.Register<SkillOutputState<EnhanceValueObject>, EnhanceOutputState>(Singleton);
            builder.Register<SkillOutputState<DamageValueObject>, DamageOutputState>(Singleton);
            builder.Register<SkillOutputState<DestroyValueObject>, DestroyOutputState>(Singleton);
            builder.Register<SkillOutputState<RecoveryValueObject>, ResetOutputState>(Singleton);
            builder.Register<SkillOutputState<RestoreValueObject>, RestoreOutputState>(Singleton);
            builder.Register<SkillOutputState<SlipValueObject>, SlipOutputState>(Singleton);
            builder.Register<SkillStopState<AilmentValueObject>>(Singleton);
            builder.Register<SkillStopState<DestroyValueObject>>(Singleton);
            builder.Register<SkillStopState<DamageValueObject>>(Singleton);
            builder.Register<SkillStopState<BuffValueObject>>(Singleton);
            builder.Register<SkillStopState<CureValueObject>>(Singleton);
            builder.Register<SkillStopState<EnhanceValueObject>>(Singleton);
            builder.Register<SkillStopState<RecoveryValueObject>>(Singleton);
            builder.Register<SkillStopState<RestoreValueObject>>(Singleton);
            builder.Register<SkillStopState<SlipValueObject>>(Singleton);
            builder.Register<SkillBreakState<DamageValueObject>>(Singleton);
            builder.Register<CharacterDeadState<DamageValueObject>>(Singleton);

            #endregion

            #region RegisterUseCase

            builder.Register<CharacterDeadUseCase>(Singleton);
            builder.Register<DamageOutputUseCase>(Singleton);
            builder.Register<EnemySelectActionUseCase>(Singleton);
            builder.Register<SkillUseCase<DamageValueObject>, SkillUseCase<DamageValueObject>>(Singleton);
            builder.Register<SkillUseCase<AilmentValueObject>, SkillUseCase<AilmentValueObject>>(Singleton);
            builder.Register<SkillUseCase<DestroyValueObject>, SkillUseCase<DestroyValueObject>>(Singleton);
            builder.Register<SkillUseCase<BuffValueObject>, SkillUseCase<BuffValueObject>>(Singleton);
            builder.Register<SkillUseCase<CureValueObject>, SkillUseCase<CureValueObject>>(Singleton);
            builder.Register<SkillUseCase<EnhanceValueObject>, SkillUseCase<EnhanceValueObject>>(Singleton);
            builder.Register<SkillUseCase<SlipValueObject>, SkillUseCase<SlipValueObject>>(Singleton);
            builder.Register<SkillUseCase<RecoveryValueObject>, SkillUseCase<RecoveryValueObject>>(Singleton);
            builder.Register<SkillUseCase<RestoreValueObject>, SkillUseCase<RestoreValueObject>>(Singleton);
            builder.Register<InitializeBattleUseCase>(Singleton);
            builder.Register<InitializeEnemyUseCase>(Singleton);
            builder.Register<InitializePlayerUseCase>(Singleton);
            builder.Register<OrderUseCase>(Singleton);
            builder.Register<PlayerSelectActionUseCase>(Singleton);
            builder.Register<PlayerSelectSkillUseCase>(Singleton);
            builder.Register<SkillUseCaseInTurnState>(Singleton);
            builder.Register<SlipUseCase>(Singleton);
            builder.Register<ResetAilmentUseCase>(Singleton);

            #endregion

            #region RegisterService

            builder.Register<ActionTimeService>(Singleton);
            builder.Register<IActualTargetIdPickerService, ActualTargetIdPickerService>(Singleton);
            builder.Register<IAilmentResetService, AilmentResetService>(Singleton);
            builder.Register<AttackCounterService>(Singleton);
            builder.Register<AttacksWeakPointEvaluatorService>(Singleton);
            builder.Register<BuffTurnService>(Singleton);
            builder.Register<ToSkillCodeService>(Singleton);
            builder.Register<ICharacterCreatorService, CharacterCreatorService>(Singleton);
            builder.Register<CharacterPropertyFactoryService>(Singleton);
            builder.Register<CureEvaluatorService>(Singleton);
            builder.Register<DamageEvaluatorService>(Singleton);
            builder.Register<IDeadCharacterService, DeadCharacterService>(Singleton);
            builder.Register<IDestroyResetService, DestroyResetService>(Singleton);
            builder.Register<IHitPointService, HitPointService>(Singleton);
            builder.Register<IsHitEvaluatorService>(Singleton);
            builder.Register<OrderService>(Singleton);
            builder.Register<IPlayerSkillService, PlayerSkillService>(Singleton);
            builder.Register<ITechnicalPointService, TechnicalPointService>(Singleton);
            builder.Register<SlipDamageRegistererService>(Singleton);
            builder.Register<ISlipResetService, SlipResetService>(Singleton);
            builder.Register<ISpeedService, SpeedService>(Singleton);
            builder.Register<ITargetService, TargetService>(Singleton);
            builder.Register<TurnService>(Singleton);
            builder.Register<ISkillService<AilmentValueObject>, AilmentService>(Singleton);
            builder.Register<ISkillService<CureValueObject>, CureService>(Singleton);
            builder.Register<ISkillService<DamageValueObject>, DamageService>(Singleton);
            builder.Register<ISkillService<DestroyValueObject>, DestroyService>(Singleton);
            builder.Register<ISkillService<BuffValueObject>, BuffService>(Singleton);
            builder.Register<ISkillService<EnhanceValueObject>, EnhanceService>(Singleton);
            builder.Register<ISkillService<SlipValueObject>, SlipService>(Singleton);
            builder.Register<ISkillService<RecoveryValueObject>, ResetService>(Singleton);
            builder.Register<ISkillService<RestoreValueObject>, RestoreService>(Singleton);

            #endregion

            #region RegisterDomainService

            builder.Register<AilmentDomainService>(Singleton);
            builder.Register<BattleLogDomainService>(Singleton);
            builder.Register<BattleLoggerService>(Singleton);
            builder.Register<EnemiesDomainService>(Singleton);
            builder.Register<OrderItemsDomainService>(Singleton);
            builder.Register<PlayerDomainService>(Singleton);
            builder.Register<SlipDomainService>(Singleton);

            #endregion

            #region RegisterEntryPoint

            builder.Register<BattleStateMachine>(Singleton)
                .AsImplementedInterfaces();

            #endregion

            builder.RegisterComponentInHierarchy<BattleSceneInitializer>();
        }
    }
}