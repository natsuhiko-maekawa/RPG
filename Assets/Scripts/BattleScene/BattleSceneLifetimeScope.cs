using BattleScene.DataAccess;
using BattleScene.DataAccess.Collection;
using BattleScene.DataAccess.Dto;
using BattleScene.DataAccess.Factory;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.Framework.Input;
using BattleScene.Framework.View;
using BattleScene.InterfaceAdapter;
using BattleScene.InterfaceAdapter.Facade;
using BattleScene.InterfaceAdapter.Presenter;
using BattleScene.InterfaceAdapter.PrimeSkill;
using BattleScene.InterfaceAdapter.ReactivePresenter;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.InterfaceAdapter.Skill;
using BattleScene.InterfaceAdapter.State.Battle;
using BattleScene.InterfaceAdapter.State.PrimeSkill;
using BattleScene.InterfaceAdapter.State.Turn;
using BattleScene.InterfaceAdapter.StateMachine;
using BattleScene.UseCases.IService;
using BattleScene.UseCases.IUseCase;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.Service.Order;
using BattleScene.UseCases.UseCase;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using static VContainer.Lifetime;
using PoisoningSkill = BattleScene.InterfaceAdapter.Skill.PoisoningSkill;

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
                builder.RegisterComponentInHierarchy<IEnemiesRegistererService>();
                builder.RegisterComponentInHierarchy<IEnemySkillSelectorService>();
                builder.RegisterComponentInHierarchy<IMyRandomService>();
                builder.Register<MyRandomService>(Singleton);
            }
            else
            {
                #region RegisterService

                builder.Register<IEnemiesRegistererService, EnemiesRegistererService>(Singleton);
                builder.Register<IEnemySkillSelectorService, EnemySkillSelectorService>(Singleton);
                builder.Register<IMyRandomService, MyRandomService>(Singleton);

                #endregion
            }

            #region RegisterView

            builder.RegisterComponentInHierarchy<EnemiesView>();
            builder.RegisterComponentInHierarchy<InfoView>();
            builder.RegisterComponentInHierarchy<BattleSceneInput>();
            builder.RegisterComponentInHierarchy<GridView>();
            builder.RegisterComponentInHierarchy<MessageView>();
            builder.RegisterComponentInHierarchy<OrderView>();
            builder.RegisterComponentInHierarchy<PlayerAttackCountView>();
            builder.RegisterComponentInHierarchy<PlayerView>();
            builder.RegisterComponentInHierarchy<PlayerStatusView>();
            builder.RegisterComponentInHierarchy<SelectSkillView>();
            builder.RegisterComponentInHierarchy<TargetView>();

            #endregion

            #region RegisterPresenter

            builder.Register<AttackCountViewPresenter>(Singleton);
            builder.Register<CureViewPresenter>(Singleton);
            builder.Register<DamageViewPresenter>(Singleton);
            builder.Register<EnemyImagePresenter>(Singleton);
            builder.Register<GridViewPresenter>(Singleton);
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
            builder.Register<IReactive<CharacterEntity>, HitPointBarViewReactivePresenter>(Singleton);
            builder.Register<IReactive<CharacterEntity>, TechnicalPointBarViewReactivePresenter>(Singleton);
            builder.Register<IReactive<SlipEntity>, SlipViewReactivePresenter>(Singleton);

            #endregion

            #region RegisterFacade

            builder.Register<AilmentOutputFacade>(Singleton);
            builder.Register<BuffOutput>(Singleton);
            builder.Register<CureOutput>(Singleton);
            builder.Register<EnhanceOutput>(Singleton);
            builder.Register<ResetAilmentOutputFacade>(Singleton);
            builder.Register<DamageOutputFacade>(Singleton);
            builder.Register<DestroyOutputFacade>(Singleton);
            builder.Register<ResetOutputFacade>(Singleton);
            builder.Register<RestoreOutputFacade>(Singleton);
            builder.Register<SkillOutputFacade>(Singleton);
            builder.Register<SlipDamageOutputFacade>(Singleton);
            builder.Register<SlipOutputFacade>(Singleton);

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
            builder.RegisterComponentInHierarchy<IResource<PlayerImagePathDto, PlayerImageCode>>();
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

            #region RegisterSkillElement

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

            builder.Register<ICollection<AilmentEntity, (CharacterId, AilmentCode)>, 
                Collection<AilmentEntity, (CharacterId, AilmentCode)>>(Singleton);
            builder.Register<ICollection<BattleLogEntity, BattleLogId>, Collection<BattleLogEntity, BattleLogId>>(
                Singleton);
            builder.Register<ICollection<BodyPartEntity, (CharacterId, BodyPartCode)>, 
                Collection<BodyPartEntity, (CharacterId, BodyPartCode)>>(Singleton);
            builder.Register<ICollection<BuffEntity, (CharacterId, BuffCode)>, 
                Collection<BuffEntity, (CharacterId, BuffCode)>>(Singleton);
            builder.Register<ICollection<CharacterEntity, CharacterId>, Collection<CharacterEntity, CharacterId>>(
                Singleton);
            builder.Register<ICollection<EnhanceEntity, (CharacterId, EnhanceCode)>, 
                Collection<EnhanceEntity, (CharacterId, EnhanceCode)>>(Singleton);
            builder.Register<ICollection<OrderedItemEntity, OrderId>, Collection<OrderedItemEntity, OrderId>>(
                Singleton);
            builder.Register<ICollection<SlipEntity, SlipCode>, Collection<SlipEntity, SlipCode>>(Singleton);
            builder.Register<ICollection<TurnEntity, TurnId>, Collection<TurnEntity, TurnId>>(Singleton);

            #endregion

            #region RegisterInterfaceAdapterService

            builder.Register<ActorService>(Singleton);
            builder.Register<MessageCodeConverterService>(Singleton);
            builder.Register<PrimeSkillStateMachine>(Singleton);
            builder.Register<ToIndexService>(Singleton);

            #endregion

            #region RegisterState

            // BattleState
            builder.Register<InitializeBattleState>(Singleton);
            builder.Register<InitializePlayerState>(Singleton);
            builder.Register<InitializeEnemyState>(Singleton);
            builder.Register<TurnState>(Singleton);
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
            builder.Register<AdvanceTurnState>(Singleton);
            builder.Register<TurnStopState>(Singleton);
            // PrimeSkillState
            builder.Register<PrimeSkillStartState<AilmentValueObject>>(Singleton);
            builder.Register<PrimeSkillStartState<DestroyValueObject>>(Singleton);
            builder.Register<PrimeSkillStartState<DamageValueObject>>(Singleton);
            builder.Register<PrimeSkillStartState<BuffValueObject>>(Singleton);
            builder.Register<PrimeSkillStartState<CureValueObject>>(Singleton);
            builder.Register<PrimeSkillStartState<EnhanceValueObject>>(Singleton);
            builder.Register<PrimeSkillStartState<RecoveryValueObject>>(Singleton);
            builder.Register<PrimeSkillStartState<RestoreValueObject>>(Singleton);
            builder.Register<PrimeSkillStartState<SlipValueObject>>(Singleton);
            builder.Register<PrimeSkillOutputState<AilmentValueObject>, AilmentOutputState>(Singleton);
            builder.Register<PrimeSkillOutputState<BuffValueObject>, BuffOutputState>(Singleton);
            builder.Register<PrimeSkillOutputState<CureValueObject>, CureOutputState>(Singleton);
            builder.Register<PrimeSkillOutputState<EnhanceValueObject>, EnhanceOutputState>(Singleton);
            builder.Register<PrimeSkillOutputState<DamageValueObject>, DamageOutputState>(Singleton);
            builder.Register<PrimeSkillOutputState<DestroyValueObject>, DestroyOutputState>(Singleton);
            builder.Register<PrimeSkillOutputState<RecoveryValueObject>, ResetOutputState>(Singleton);
            builder.Register<PrimeSkillOutputState<RestoreValueObject>, RestoreOutputState>(Singleton);
            builder.Register<PrimeSkillOutputState<SlipValueObject>, SlipOutputState>(Singleton);
            builder.Register<PrimeSkillStopState<AilmentValueObject>>(Singleton);
            builder.Register<PrimeSkillStopState<DestroyValueObject>>(Singleton);
            builder.Register<PrimeSkillStopState<DamageValueObject>>(Singleton);
            builder.Register<PrimeSkillStopState<BuffValueObject>>(Singleton);
            builder.Register<PrimeSkillStopState<CureValueObject>>(Singleton);
            builder.Register<PrimeSkillStopState<EnhanceValueObject>>(Singleton);
            builder.Register<PrimeSkillStopState<RecoveryValueObject>>(Singleton);
            builder.Register<PrimeSkillStopState<RestoreValueObject>>(Singleton);
            builder.Register<PrimeSkillStopState<SlipValueObject>>(Singleton);
            builder.Register<PrimeSkillBreakState<DamageValueObject>>(Singleton);

            #endregion

            #region RegisterUseCase

            builder.Register<EnemySelectActionUseCase>(Singleton);
            builder.Register<IPrimeSkillUseCase<DamageValueObject>, PrimeSkillUseCase<DamageValueObject>>(Singleton);
            builder.Register<IPrimeSkillUseCase<AilmentValueObject>, PrimeSkillUseCase<AilmentValueObject>>(Singleton);
            builder.Register<IPrimeSkillUseCase<DestroyValueObject>, PrimeSkillUseCase<DestroyValueObject>>(Singleton);
            builder.Register<IPrimeSkillUseCase<BuffValueObject>, PrimeSkillUseCase<BuffValueObject>>(Singleton);
            builder.Register<IPrimeSkillUseCase<CureValueObject>, PrimeSkillUseCase<CureValueObject>>(Singleton);
            builder.Register<IPrimeSkillUseCase<EnhanceValueObject>, PrimeSkillUseCase<EnhanceValueObject>>(Singleton);
            builder.Register<IPrimeSkillUseCase<SlipValueObject>, PrimeSkillUseCase<SlipValueObject>>(Singleton);
            builder.Register<IPrimeSkillUseCase<RecoveryValueObject>, 
                PrimeSkillUseCase<RecoveryValueObject>>(Singleton);
            builder.Register<IPrimeSkillUseCase<RestoreValueObject>, PrimeSkillUseCase<RestoreValueObject>>(Singleton);
            builder.Register<InitializeEnemyUseCase>(Singleton);
            builder.Register<InitializePlayerUseCase>(Singleton);
            builder.Register<OrderUseCase>(Singleton);
            builder.Register<PlayerSelectActionUseCase>(Singleton);
            builder.Register<PlayerSelectSkillUseCase>(Singleton);
            builder.Register<SkillUseCase>(Singleton);
            builder.Register<SlipUseCase>(Singleton);
            builder.Register<ResetAilmentUseCase>(Singleton);

            #endregion

            #region RegisterService

            builder.Register<ActionTimeService>(Singleton);
            builder.Register<ActualTargetIdPickerService>(Singleton);
            builder.Register<IAilmentResetService, AilmentResetService>(Singleton);
            builder.Register<AttackCounterService>(Singleton);
            builder.Register<AttacksWeakPointEvaluatorService>(Singleton);
            builder.Register<BuffTurnService>(Singleton);
            builder.Register<ToSkillCodeService>(Singleton);
            builder.Register<ICharacterCreatorService, CharacterCreatorService>(Singleton);
            builder.Register<CharacterPropertyFactoryService>(Singleton);
            builder.Register<CureEvaluatorService>(Singleton);
            builder.Register<DamageEvaluatorService>(Singleton);
            builder.Register<IHitPointService, HitPointService>(Singleton);
            builder.Register<IsHitEvaluatorService>(Singleton);
            builder.Register<OrderService>(Singleton);
            builder.Register<ITechnicalPointService, TechnicalPointService>(Singleton);
            builder.Register<SlipDamageGeneratorService>(Singleton);
            builder.Register<ISpeedService, SpeedService>(Singleton);
            builder.Register<ITargetService, TargetService>(Singleton);
            builder.Register<TurnService>(Singleton);
            builder.Register<IPrimeSkillService<AilmentValueObject>, AilmentService>(Singleton);
            builder.Register<IPrimeSkillService<CureValueObject>, CureService>(Singleton);
            builder.Register<IPrimeSkillService<DamageValueObject>, DamageService>(Singleton);
            builder.Register<IPrimeSkillService<DestroyValueObject>, DestroyService>(Singleton);
            builder.Register<IPrimeSkillService<BuffValueObject>, BuffService>(Singleton);
            builder.Register<IPrimeSkillService<EnhanceValueObject>, EnhanceService>(Singleton);
            builder.Register<IPrimeSkillService<SlipValueObject>, SlipService>(Singleton);
            builder.Register<IPrimeSkillService<RecoveryValueObject>, ResetService>(Singleton);
            builder.Register<IPrimeSkillService<RestoreValueObject>, RestoreService>(Singleton);

            #endregion

            #region RegisterDomainService

            builder.Register<AilmentDomainService>(Singleton);
            builder.Register<BattleLogDomainService>(Singleton);
            builder.Register<BattleLoggerService>(Singleton);
            builder.Register<BodyPartDomainService>(Singleton);
            builder.Register<EnemiesDomainService>(Singleton);
            builder.Register<OrderedItemsDomainService>(Singleton);
            builder.Register<PlayerDomainService>(Singleton);
            builder.Register<SlipDomainService>(Singleton);

            #endregion

            #region RegisterEntryPoint

            builder.RegisterEntryPoint<BattleStateMachine>();

            #endregion
        }
    }
}