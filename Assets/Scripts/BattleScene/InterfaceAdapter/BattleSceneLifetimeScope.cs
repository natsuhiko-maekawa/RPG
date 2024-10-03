using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.DataAccess.Factory;
using BattleScene.DataAccess.Repository;
using BattleScene.DataAccess.Resource;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.Framework.Input;
using BattleScene.Framework.View;
using BattleScene.InterfaceAdapter.Facade;
using BattleScene.InterfaceAdapter.Presenter;
using BattleScene.InterfaceAdapter.PrimeSkill;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.InterfaceAdapter.Skill;
using BattleScene.InterfaceAdapter.State.Battle;
using BattleScene.InterfaceAdapter.State.PrimeSkill;
using BattleScene.InterfaceAdapter.State.Turn;
using BattleScene.InterfaceAdapter.StateMachine;
using BattleScene.UseCases.Interface;
using BattleScene.UseCases.IService;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.UseCase;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using TechnicalPointBarViewPresenter = BattleScene.InterfaceAdapter.Presenter.TechnicalPointBarViewPresenter;

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
                builder.Register<IEnemySkillSelectorService, EnemySpecificSkillSelectorService>(Lifetime.Singleton);
                builder.Register<IMyRandomService, MyRandomService>(Lifetime.Singleton);
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
            builder.Register<IObserver<AilmentEntity>, AilmentViewPresenter>(Lifetime.Singleton);
            builder.Register<IObserver<BodyPartEntity>, BodyPartViewPresenter>(Lifetime.Singleton);
            builder.Register<IObserver<CharacterEntity>, HitPointBarViewPresenter>(Lifetime.Singleton);
            builder.Register<IObserver<CharacterEntity>, TechnicalPointBarViewPresenter>(Lifetime.Singleton);
            builder.Register<IObserver<SlipEntity>, SlipViewPresenter>(Lifetime.Singleton);
            #endregion

            #region RegisterFacade
            builder.Register<AilmentOutputFacade>(Lifetime.Singleton);
            builder.Register<DamageOutputFacade>(Lifetime.Singleton);
            builder.Register<DestroyOutputFacade>(Lifetime.Singleton);
            builder.Register<RestoreOutputFacade>(Lifetime.Singleton);
            builder.Register<SkillOutputFacade>(Lifetime.Singleton);
            builder.Register<SlipOutputFacade>(Lifetime.Singleton);
            #endregion
            
            #region RegisterResource
            builder.RegisterComponentInHierarchy<IResource<AilmentPropertyDto, AilmentCode>>();
            builder.RegisterComponentInHierarchy<IAilmentViewResource>();
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
            builder.RegisterComponentInHierarchy<IResource<SkillPropertyDto, SkillCode>>();
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
            builder.Register<PunchSkill>(Lifetime.Transient);
            builder.Register<PutScytheSkill>(Lifetime.Transient);
            builder.Register<RaikiriSkill>(Lifetime.Transient);
            builder.Register<RandomShotsSkill>(Lifetime.Transient);
            builder.Register<ShichishitouSkill>(Lifetime.Transient);
            builder.Register<SilverBulletSkill>(Lifetime.Transient);
            builder.Register<SmokeBombSkill>(Lifetime.Transient);
            builder.Register<StarShellSkill>(Lifetime.Transient);
            builder.Register<StringerSkill>(Lifetime.Transient);
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
            builder.Register<IFactory<PropertyValueObject, CharacterTypeCode>, PropertyFactory>(Lifetime.Singleton);
            builder.Register<IFactory<PlayerPropertyValueObject, CharacterTypeCode>, PlayerPropertyFactory>
                (Lifetime.Singleton);
            builder.Register<IFactory<SkillValueObject, SkillCode>, SkillFactory>(Lifetime.Singleton);
            #endregion
            
            #region RegisterRepository
            builder.Register<IRepository<AilmentEntity, (CharacterId, AilmentCode)>, Repository<AilmentEntity, (CharacterId, AilmentCode)>>(
                Lifetime.Singleton);
            builder.Register<IRepository<BattleLogEntity, BattleLogId>, Repository<BattleLogEntity, BattleLogId>>(
                Lifetime.Singleton);
            builder.Register<IRepository<BodyPartEntity, (CharacterId, BodyPartCode)>, Repository<BodyPartEntity, (CharacterId, BodyPartCode)>>(
                Lifetime.Singleton);
            builder.Register<IRepository<BuffEntity, (CharacterId, BuffCode)>, Repository<BuffEntity, (CharacterId, BuffCode)>>(
                Lifetime.Singleton);
            builder.Register<IRepository<CharacterEntity, CharacterId>, Repository<CharacterEntity, CharacterId>>(Lifetime.Singleton);
            builder.Register<IRepository<OrderedItemEntity, OrderId>, Repository<OrderedItemEntity, OrderId>>(
                Lifetime.Singleton);
            builder.Register<IRepository<SlipEntity, SlipDamageCode>, Repository<SlipEntity, SlipDamageCode>>(
                Lifetime.Singleton);
            builder.Register<IRepository<TurnEntity, TurnId>, Repository<TurnEntity, TurnId>>(Lifetime.Singleton);
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
            builder.Register<SelectTargetState>(Lifetime.Singleton);
            builder.Register<EnemySelectSkillState>(Lifetime.Singleton);
            builder.Register<SkillState>(Lifetime.Singleton);
            builder.Register<TurnStopState>(Lifetime.Singleton);
            // PrimeSkillState
            builder.Register<PrimeSkillStartState<AilmentParameterValueObject, AilmentValueObject>>(Lifetime.Singleton);
            builder.Register<PrimeSkillStartState<DestroyParameterValueObject, DestroyValueObject>>(Lifetime.Singleton);
            builder.Register<PrimeSkillStartState<DamageParameterValueObject, DamageValueObject>>(Lifetime.Singleton);
            builder.Register<PrimeSkillStartState<BuffParameterValueObject, BuffValueObject>>(Lifetime.Singleton);
            builder.Register<PrimeSkillStartState<RestoreParameterValueObject, RestoreValueObject>>(Lifetime.Singleton);
            builder.Register<PrimeSkillStartState<SlipParameterValueObject, SlipValueObject>>(Lifetime.Singleton);
            builder.Register<PrimeSkillOutputState<AilmentParameterValueObject, AilmentValueObject>, AilmentOutputState>(Lifetime.Singleton);
            builder.Register<PrimeSkillOutputState<BuffParameterValueObject, BuffValueObject>, BuffMessageState>(Lifetime.Singleton);
            builder.Register<PrimeSkillOutputState<DamageParameterValueObject, DamageValueObject>, DamageOutputState>(Lifetime.Singleton);
            builder.Register<PrimeSkillOutputState<DestroyParameterValueObject, DestroyValueObject>, DestroyOutputState>(Lifetime.Singleton);
            builder.Register<PrimeSkillOutputState<RestoreParameterValueObject, RestoreValueObject>, RestoreOutputState>(Lifetime.Singleton);
            builder.Register<PrimeSkillOutputState<SlipParameterValueObject, SlipValueObject>, SlipOutputState>(Lifetime.Singleton);
            builder.Register<PrimeSkillStopState<AilmentParameterValueObject, AilmentValueObject>>(Lifetime.Singleton);
            builder.Register<PrimeSkillStopState<DestroyParameterValueObject, DestroyValueObject>>(Lifetime.Singleton);
            builder.Register<PrimeSkillStopState<DamageParameterValueObject, DamageValueObject>>(Lifetime.Singleton);
            builder.Register<PrimeSkillStopState<BuffParameterValueObject, BuffValueObject>>(Lifetime.Singleton);
            builder.Register<PrimeSkillStopState<RestoreParameterValueObject, RestoreValueObject>>(Lifetime.Singleton);
            builder.Register<PrimeSkillStopState<SlipParameterValueObject, SlipValueObject>>(Lifetime.Singleton);
            builder.Register<PrimeSkillBreakState<DamageParameterValueObject, DamageValueObject>>(Lifetime.Singleton);
            #endregion

            #region RegisterUseCase
            builder.Register<IPrimeSkill<DamageParameterValueObject, DamageValueObject>, 
                PrimeSkill<DamageParameterValueObject, DamageValueObject>>(Lifetime.Singleton);
            builder.Register<IPrimeSkill<AilmentParameterValueObject, AilmentValueObject>, 
                PrimeSkill<AilmentParameterValueObject, AilmentValueObject>>(Lifetime.Singleton);
            builder.Register<IPrimeSkill<DestroyParameterValueObject, DestroyValueObject>, 
                PrimeSkill<DestroyParameterValueObject, DestroyValueObject>>(Lifetime.Singleton);
            builder.Register<IPrimeSkill<BuffParameterValueObject, BuffValueObject>, 
                PrimeSkill<BuffParameterValueObject, BuffValueObject>>(Lifetime.Singleton);
            builder.Register<IPrimeSkill<SlipParameterValueObject, SlipValueObject>, 
                PrimeSkill<SlipParameterValueObject, SlipValueObject>>(Lifetime.Singleton);
            builder.Register<IPrimeSkill<RestoreParameterValueObject, RestoreValueObject>, 
                PrimeSkill<RestoreParameterValueObject, RestoreValueObject>>(Lifetime.Singleton);
            #endregion

            #region RegisterService
            builder.Register<ActionTimeService>(Lifetime.Singleton);
            builder.Register<ActualTargetIdPickerService>(Lifetime.Singleton);
            builder.Register<AilmentGeneratorService>(Lifetime.Singleton);
            builder.Register<AilmentRegistererService>(Lifetime.Singleton);
            builder.Register<AttackCounterService>(Lifetime.Singleton);
            builder.Register<AttacksWeakPointEvaluatorService>(Lifetime.Singleton);
            builder.Register<BuffGeneratorService>(Lifetime.Singleton);
            builder.Register<BuffRegistererService>(Lifetime.Singleton);
            builder.Register<CharacterPropertyFactoryService>(Lifetime.Singleton);
            builder.Register<DamageEvaluatorService>(Lifetime.Singleton);
            builder.Register<IsHitEvaluatorService>(Lifetime.Singleton);
            builder.Register<OrderService>(Lifetime.Singleton);
            builder.Register<RestoreGeneratorService>(Lifetime.Singleton);
            builder.Register<SkillExecutorService>(Lifetime.Singleton);
            builder.Register<SlipDamageGeneratorService>(Lifetime.Singleton);
            builder.Register<SlipGeneratorService>(Lifetime.Singleton);
            builder.Register<SlipRegistererService>(Lifetime.Singleton);
            builder.Register<TurnInitializerService>(Lifetime.Singleton);
            builder.Register<IPrimeSkillGeneratorService<AilmentParameterValueObject, AilmentValueObject>, AilmentGeneratorService>(Lifetime.Singleton);
            builder.Register<IPrimeSkillRegistererService<AilmentValueObject>, AilmentRegistererService>(Lifetime.Singleton);
            builder.Register<IPrimeSkillGeneratorService<DamageParameterValueObject, DamageValueObject>, DamageGeneratorService>(Lifetime.Singleton);
            builder.Register<IPrimeSkillRegistererService<DamageValueObject>, DamageRegistererService>(Lifetime.Singleton);
            builder.Register<IPrimeSkillGeneratorService<DestroyParameterValueObject, DestroyValueObject>, DestroyGeneratorService>(Lifetime.Singleton);
            builder.Register<IPrimeSkillRegistererService<DestroyValueObject>, DestroyRegistererService>(Lifetime.Singleton);
            builder.Register<IPrimeSkillGeneratorService<BuffParameterValueObject, BuffValueObject>, BuffGeneratorService>(Lifetime.Singleton);
            builder.Register<IPrimeSkillRegistererService<BuffValueObject>, BuffRegistererService>(Lifetime.Singleton);
            builder.Register<IPrimeSkillGeneratorService<SlipParameterValueObject, SlipValueObject>, SlipGeneratorService>(Lifetime.Singleton);
            builder.Register<IPrimeSkillRegistererService<SlipValueObject>, SlipRegistererService>(Lifetime.Singleton);
            builder.Register<IPrimeSkillGeneratorService<RestoreParameterValueObject, RestoreValueObject>, RestoreGeneratorService>(Lifetime.Singleton);
            builder.Register<IPrimeSkillRegistererService<RestoreValueObject>, RestoreRegistererService>(Lifetime.Singleton);

            #endregion

            #region RegisterDomainService
            builder.Register<AilmentDomainService>(Lifetime.Singleton);
            builder.Register<BattleLogDomainService>(Lifetime.Singleton);
            builder.Register<BattleLoggerService>(Lifetime.Singleton);
            builder.Register<BodyPartDomainService>(Lifetime.Singleton);
            builder.Register<BuffDomainService>(Lifetime.Singleton);
            builder.Register<EnemiesDomainService>(Lifetime.Singleton);
            builder.Register<OrderedItemsDomainService>(Lifetime.Singleton);
            builder.Register<PlayerDomainService>(Lifetime.Singleton);
            builder.Register<SlipDomainService>(Lifetime.Singleton);
            builder.Register<TargetDomainService>(Lifetime.Singleton);
            #endregion

            #region RegisterEntryPoint
            builder.RegisterEntryPoint<BattleStateMachine>();
            #endregion
        }
    }
}