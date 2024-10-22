using BattleScene.DataAccess;
using BattleScene.DataAccess.Collection;
using BattleScene.DataAccess.Dto;
using BattleScene.DataAccess.Factory;
using BattleScene.DataAccess.Resource;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IDomainService;
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
                builder.Register<MyRandomService>(Lifetime.Singleton);
            }
            else
            {
                #region RegisterService

                builder.Register<IEnemiesRegistererService, EnemiesRegistererService>(Lifetime.Singleton);
                builder.Register<IEnemySkillSelectorService, EnemySkillSelectorService>(Lifetime.Singleton);
                builder.Register<IMyRandomService, MyRandomService>(Lifetime.Singleton);

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

            builder.Register<AttackCountViewPresenter>(Lifetime.Singleton);
            builder.Register<DamageViewPresenter>(Lifetime.Singleton);
            builder.Register<EnemyImagePresenter>(Lifetime.Singleton);
            builder.Register<GridViewPresenter>(Lifetime.Singleton);
            builder.Register<MessageViewPresenter>(Lifetime.Singleton);
            builder.Register<OrderViewPresenter>(Lifetime.Singleton);
            builder.Register<PlayerImageViewPresenter>(Lifetime.Singleton);
            builder.Register<RestoreViewPresenter>(Lifetime.Singleton);
            builder.Register<SkillViewPresenter>(Lifetime.Singleton);
            builder.Register<TargetViewPresenter>(Lifetime.Singleton);
            builder.Register<VibrationViewPresenter>(Lifetime.Singleton);

            #endregion

            #region RegisterReactivePresenter

            builder.Register<IReactive<AilmentEntity>, AilmentViewReactivePresenter>(Lifetime.Singleton);
            builder.Register<IReactive<BodyPartEntity>, BodyPartViewReactivePresenter>(Lifetime.Singleton);
            builder.Register<IReactive<BuffEntity>, BuffViewReactivePresenter>(Lifetime.Singleton);
            builder.Register<IReactive<CharacterEntity>, HitPointBarViewReactivePresenter>(Lifetime.Singleton);
            builder.Register<IReactive<CharacterEntity>, TechnicalPointBarViewReactivePresenter>(Lifetime.Singleton);
            builder.Register<IReactive<SlipEntity>, SlipViewReactivePresenter>(Lifetime.Singleton);

            #endregion

            #region RegisterFacade

            builder.Register<AilmentOutputFacade>(Lifetime.Singleton);
            builder.Register<ResetAilmentOutputFacade>(Lifetime.Singleton);
            builder.Register<DamageOutputFacade>(Lifetime.Singleton);
            builder.Register<DestroyOutputFacade>(Lifetime.Singleton);
            builder.Register<RestoreOutputFacade>(Lifetime.Singleton);
            builder.Register<SkillOutputFacade>(Lifetime.Singleton);
            builder.Register<SlipDamageOutputFacade>(Lifetime.Singleton);
            builder.Register<SlipOutputFacade>(Lifetime.Singleton);

            #endregion

            #region RegisterResource

            builder.RegisterComponentInHierarchy<IResource<AilmentPropertyDto, AilmentCode>>();
            builder.RegisterComponentInHierarchy<IResource<AilmentViewDto, AilmentCode, SlipCode>>();
            builder.RegisterComponentInHierarchy<IResource<BattlePropertyDto>>();
            builder.RegisterComponentInHierarchy<IResource<BodyPartPropertyDto, BodyPartCode>>();
            builder.Register<IResource<BodyPartViewDto, BodyPartCode>, BodyPartViewResource>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<IResource<BuffViewDto, BuffCode>>();
            builder.RegisterComponentInHierarchy<IResource<CharacterPropertyDto, CharacterTypeCode>>();
            builder.RegisterComponentInHierarchy<IResource<EnemyViewDto, CharacterTypeCode>>();
            builder.RegisterComponentInHierarchy<IResource<MessageDto, MessageCode>>();
            builder.RegisterComponentInHierarchy<IResource<PlayerViewDto, CharacterTypeCode>>();
            builder.RegisterComponentInHierarchy<IResource<PlayerPropertyDto, CharacterTypeCode>>();
            builder.RegisterComponentInHierarchy<IResource<PlayerImagePathDto, PlayerImageCode>>();
            builder.RegisterComponentInHierarchy<IResource<SkillViewDto, SkillCode>>();

            #endregion

            #region RegisterSkill

            builder.Register<AfterimageSkill>(Lifetime.Transient);
            builder.Register<AttackSkill>(Lifetime.Transient);
            builder.Register<BiteSkill>(Lifetime.Transient);
            builder.Register<BiteOffSkill>(Lifetime.Transient);
            builder.Register<ConfusionSkill>(Lifetime.Transient);
            builder.Register<CutUpSkill>(Lifetime.Transient);
            builder.Register<DefenceSkill>(Lifetime.Transient);
            builder.Register<FieldRationSkill>(Lifetime.Transient);
            builder.Register<FirstAidSkill>(Lifetime.Transient);
            builder.Register<FlameThrowSkill>(Lifetime.Transient);
            builder.Register<HonzougakuSkill>(Lifetime.Transient);
            builder.Register<IshinhouSkill>(Lifetime.Transient);
            builder.Register<KuchiyoseSkill>(Lifetime.Transient);
            builder.Register<KyoukasuigetsuSkill>(Lifetime.Transient);
            builder.Register<LiquidSkill>(Lifetime.Transient);
            builder.Register<MurasameSkill>(Lifetime.Transient);
            builder.Register<MusterStrengthSkill>(Lifetime.Transient);
            builder.Register<NadegiriSkill>(Lifetime.Transient);
            builder.Register<NumbLiquidSkill>(Lifetime.Transient);
            builder.Register<OnikoroshiSkill>(Lifetime.Transient);
            builder.Register<ParalysisSkill>(Lifetime.Transient);
            builder.Register<PoisoningSkill>(Lifetime.Transient);
            builder.Register<PunchSkill>(Lifetime.Transient);
            builder.Register<PutScytheSkill>(Lifetime.Transient);
            builder.Register<RaikiriSkill>(Lifetime.Transient);
            builder.Register<RandomShotsSkill>(Lifetime.Transient);
            builder.Register<ShichishitouSkill>(Lifetime.Transient);
            builder.Register<SilverBulletSkill>(Lifetime.Transient);
            builder.Register<SmokeBombSkill>(Lifetime.Transient);
            builder.Register<StarShellSkill>(Lifetime.Transient);
            builder.Register<StringerSkill>(Lifetime.Transient);
            builder.Register<SuffocationSkill>(Lifetime.Transient);
            builder.Register<TaserGunSkill>(Lifetime.Transient);
            builder.Register<UtsusemiSkill>(Lifetime.Transient);
            builder.Register<WabisukeSkill>(Lifetime.Transient);

            #endregion

            #region RegisterSkillElement

            builder.Register<AbsoluteConfusion>(Lifetime.Singleton);
            builder.Register<AfterImage>(Lifetime.Singleton);
            builder.Register<AlwaysHitDamage>(Lifetime.Singleton);
            builder.Register<BasicCure>(Lifetime.Singleton);
            builder.Register<BasicDamage>(Lifetime.Singleton);
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
            builder.Register<IFactory<BodyPartPropertyValueObject, BodyPartCode>, BodyPartPropertyFactory>(
                Lifetime.Singleton);
            builder.Register<IFactory<CharacterPropertyValueObject, CharacterTypeCode>, CharacterPropertyFactory>(
                Lifetime.Singleton);
            builder.Register<IFactory<PlayerPropertyValueObject, CharacterTypeCode>, PlayerPropertyFactory>
                (Lifetime.Singleton);
            builder.Register<IFactory<SkillValueObject, SkillCode>, SkillFactory>(Lifetime.Singleton);

            #endregion

            #region RegisterCollection

            builder.Register<ICollection<AilmentEntity, (CharacterId, AilmentCode)>, 
                Collection<AilmentEntity, (CharacterId, AilmentCode)>>(Lifetime.Singleton);
            builder.Register<ICollection<BattleLogEntity, BattleLogId>, Collection<BattleLogEntity, BattleLogId>>(
                Lifetime.Singleton);
            builder.Register<ICollection<BodyPartEntity, (CharacterId, BodyPartCode)>, 
                Collection<BodyPartEntity, (CharacterId, BodyPartCode)>>(Lifetime.Singleton);
            builder.Register<ICollection<BuffEntity, (CharacterId, BuffCode)>, 
                Collection<BuffEntity, (CharacterId, BuffCode)>>(Lifetime.Singleton);
            builder.Register<ICollection<CharacterEntity, CharacterId>, Collection<CharacterEntity, CharacterId>>(
                Lifetime.Singleton);
            builder.Register<ICollection<EnhanceEntity, (CharacterId, EnhanceCode)>, 
                Collection<EnhanceEntity, (CharacterId, EnhanceCode)>>(Lifetime.Singleton);
            builder.Register<ICollection<OrderedItemEntity, OrderId>, Collection<OrderedItemEntity, OrderId>>(
                Lifetime.Singleton);
            builder.Register<ICollection<SlipEntity, SlipCode>, Collection<SlipEntity, SlipCode>>(Lifetime.Singleton);
            builder.Register<ICollection<TurnEntity, TurnId>, Collection<TurnEntity, TurnId>>(Lifetime.Singleton);

            #endregion

            #region RegisterInterfaceAdapterService

            builder.Register<ActorService>(Lifetime.Singleton);
            builder.Register<MessageCodeConverterService>(Lifetime.Singleton);
            builder.Register<PrimeSkillStateMachine>(Lifetime.Singleton);
            builder.Register<ToIndexService>(Lifetime.Singleton);

            #endregion

            #region RegisterState

            // BattleState
            builder.Register<InitializeBattleState>(Lifetime.Singleton);
            builder.Register<InitializePlayerState>(Lifetime.Singleton);
            builder.Register<InitializeEnemyState>(Lifetime.Singleton);
            builder.Register<TurnState>(Lifetime.Singleton);
            // TurnState
            builder.Register<TurnStartState>(Lifetime.Singleton);
            builder.Register<PlayerSelectActionState>(Lifetime.Singleton);
            builder.Register<PlayerSelectSkillState>(Lifetime.Singleton);
            builder.Register<PlayerSelectTargetState>(Lifetime.Singleton);
            builder.Register<EnemySelectSkillState>(Lifetime.Singleton);
            builder.Register<CantActionState>(Lifetime.Singleton);
            builder.Register<SlipDamageState>(Lifetime.Singleton);
            builder.Register<ResetAilmentState>(Lifetime.Singleton);
            builder.Register<SkillState>(Lifetime.Singleton);
            builder.Register<AdvanceTurnState>(Lifetime.Singleton);
            builder.Register<TurnStopState>(Lifetime.Singleton);
            // PrimeSkillState
            builder.Register<PrimeSkillStartState<AilmentParameterValueObject>>(Lifetime.Singleton);
            builder.Register<PrimeSkillStartState<DestroyParameterValueObject>>(Lifetime.Singleton);
            builder.Register<PrimeSkillStartState<DamageParameterValueObject>>(Lifetime.Singleton);
            builder.Register<PrimeSkillStartState<BuffParameterValueObject>>(Lifetime.Singleton);
            builder.Register<PrimeSkillStartState<RestoreParameterValueObject>>(Lifetime.Singleton);
            builder.Register<PrimeSkillStartState<SlipParameterValueObject>>(Lifetime.Singleton);
            builder.Register<PrimeSkillOutputState<AilmentParameterValueObject>, AilmentOutputState>(
                Lifetime.Singleton);
            builder.Register<PrimeSkillOutputState<BuffParameterValueObject>, BuffOutputState>(Lifetime.Singleton);
            builder.Register<PrimeSkillOutputState<DamageParameterValueObject>, DamageOutputState>(Lifetime.Singleton);
            builder.Register<PrimeSkillOutputState<DestroyParameterValueObject>, DestroyOutputState>(
                Lifetime.Singleton);
            builder.Register<PrimeSkillOutputState<RestoreParameterValueObject>, RestoreOutputState>(
                Lifetime.Singleton);
            builder.Register<PrimeSkillOutputState<SlipParameterValueObject>, SlipOutputState>(Lifetime.Singleton);
            builder.Register<PrimeSkillStopState<AilmentParameterValueObject>>(Lifetime.Singleton);
            builder.Register<PrimeSkillStopState<DestroyParameterValueObject>>(Lifetime.Singleton);
            builder.Register<PrimeSkillStopState<DamageParameterValueObject>>(Lifetime.Singleton);
            builder.Register<PrimeSkillStopState<BuffParameterValueObject>>(Lifetime.Singleton);
            builder.Register<PrimeSkillStopState<RestoreParameterValueObject>>(Lifetime.Singleton);
            builder.Register<PrimeSkillStopState<SlipParameterValueObject>>(Lifetime.Singleton);
            builder.Register<PrimeSkillBreakState<DamageParameterValueObject>>(Lifetime.Singleton);

            #endregion

            #region RegisterUseCase

            builder.Register<IPrimeSkillUseCase<DamageParameterValueObject>, 
                PrimeSkillUseCase<DamageParameterValueObject>>(Lifetime.Singleton);
            builder.Register<IPrimeSkillUseCase<AilmentParameterValueObject>,
                PrimeSkillUseCase<AilmentParameterValueObject>>(Lifetime.Singleton);
            builder.Register<IPrimeSkillUseCase<DestroyParameterValueObject>,
                PrimeSkillUseCase<DestroyParameterValueObject>>(Lifetime.Singleton);
            builder.Register<IPrimeSkillUseCase<BuffParameterValueObject>,
                PrimeSkillUseCase<BuffParameterValueObject>>(Lifetime.Singleton);
            builder.Register<IPrimeSkillUseCase<SlipParameterValueObject>,
                PrimeSkillUseCase<SlipParameterValueObject>>(Lifetime.Singleton);
            builder.Register<IPrimeSkillUseCase<RestoreParameterValueObject>,
                PrimeSkillUseCase<RestoreParameterValueObject>>(Lifetime.Singleton);
            builder.Register<OrderUseCase>(Lifetime.Singleton);
            builder.Register<SlipUseCase>(Lifetime.Singleton);

            #endregion

            #region RegisterService

            builder.Register<ActionTimeService>(Lifetime.Singleton);
            builder.Register<ActualTargetIdPickerService>(Lifetime.Singleton);
            builder.Register<AilmentResetService>(Lifetime.Singleton);
            builder.Register<AttackCounterService>(Lifetime.Singleton);
            builder.Register<AttacksWeakPointEvaluatorService>(Lifetime.Singleton);
            builder.Register<BuffTurnService>(Lifetime.Singleton);
            builder.Register<ToSkillCodeService>(Lifetime.Singleton);
            builder.Register<CharacterPropertyFactoryService>(Lifetime.Singleton);
            builder.Register<DamageEvaluatorService>(Lifetime.Singleton);
            builder.Register<IsHitEvaluatorService>(Lifetime.Singleton);
            builder.Register<OrderService>(Lifetime.Singleton);
            builder.Register<SkillExecutorService>(Lifetime.Singleton);
            builder.Register<SlipDamageGeneratorService>(Lifetime.Singleton);
            builder.Register<ISpeedService, SpeedService>(Lifetime.Singleton);
            builder.Register<TurnInitializerService>(Lifetime.Singleton);
            builder.Register<IPrimeSkillService<AilmentParameterValueObject>, AilmentService>(Lifetime.Singleton);
            builder.Register<IPrimeSkillService<DamageParameterValueObject>, DamageService>(Lifetime.Singleton);
            builder.Register<IPrimeSkillService<DestroyParameterValueObject>, DestroyService>(Lifetime.Singleton);
            builder.Register<IPrimeSkillService<BuffParameterValueObject>, BuffService>(Lifetime.Singleton);
            builder.Register<IPrimeSkillService<SlipParameterValueObject>, SlipService>(Lifetime.Singleton);
            builder.Register<IPrimeSkillService<RestoreParameterValueObject>, RestoreService>(Lifetime.Singleton);

            #endregion

            #region RegisterDomainService

            builder.Register<AilmentDomainService>(Lifetime.Singleton);
            builder.Register<BattleLogDomainService>(Lifetime.Singleton);
            builder.Register<BattleLoggerService>(Lifetime.Singleton);
            builder.Register<BodyPartDomainService>(Lifetime.Singleton);
            builder.Register<IBuffDomainService, BuffDomainService>(Lifetime.Singleton);
            builder.Register<EnemiesDomainService>(Lifetime.Singleton);
            builder.Register<OrderedItemsDomainService>(Lifetime.Singleton);
            builder.Register<PlayerDomainService>(Lifetime.Singleton);
            builder.Register<SlipDomainService>(Lifetime.Singleton);
            builder.Register<TargetService>(Lifetime.Singleton);

            #endregion

            #region RegisterEntryPoint

            builder.RegisterEntryPoint<BattleStateMachine>();

            #endregion
        }
    }
}