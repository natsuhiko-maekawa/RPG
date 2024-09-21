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
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.Framework.Input;
using BattleScene.Framework.View;
using BattleScene.InterfaceAdapter.ObsoletePresenter;
using BattleScene.InterfaceAdapter.Presenter;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.InterfaceAdapter.Skill;
using BattleScene.InterfaceAdapter.Skill.SkillElement;
using BattleScene.InterfaceAdapter.State.Battle;
using BattleScene.InterfaceAdapter.State.Skill;
using BattleScene.UseCases.IService;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.View.BuffView.OutputBoundary;
using BattleScene.UseCases.View.InfoView.OutputBoundary;
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
                builder.Register<IEnemySkillSelectorService, EnemySkillSelectorService>(Lifetime.Singleton);
            }
            else
            {
                #region RegisterService
                builder.Register<IEnemiesRegistererService, EnemiesRegistererService>(Lifetime.Singleton);
                builder.Register<IEnemySkillSelectorService, EnemySkillSelectorService>(Lifetime.Singleton);
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
            builder.Register<SkillViewPresenter>(Lifetime.Singleton);
            builder.Register<TargetViewPresenter>(Lifetime.Singleton);
            builder.Register<VibrationViewPresenter>(Lifetime.Singleton);
            #endregion

            #region RegisterReactivePresenter
            builder.Register<IObserver<AilmentEntity>, AilmentViewPresenter>(Lifetime.Singleton);
            builder.Register<IObserver<BodyPartEntity>, BodyPartViewPresenter>(Lifetime.Singleton);
            builder.Register<IObserver<CharacterEntity>, HitPointBarViewPresenter>(Lifetime.Singleton);
            builder.Register<IObserver<CharacterEntity>, TechnicalPointBarViewPresenter>(Lifetime.Singleton);
            #endregion
            
            #region RegisterObsoletePresenter
            builder.Register<IBuffViewPresenter, BuffViewPresenter>(Lifetime.Singleton);
            builder.Register<IInfoViewPresenter, InfoViewPresenter>(Lifetime.Singleton);
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
            builder.Register<IRepository<SlipDamageEntity, SlipDamageCode>, Repository<SlipDamageEntity, SlipDamageCode>>(
                Lifetime.Singleton);
            builder.Register<IRepository<TurnEntity, TurnId>, Repository<TurnEntity, TurnId>>(Lifetime.Singleton);
            #endregion

            #region RegisterInterfaceAdapterService
            builder.Register<MessageCodeConverterService>(Lifetime.Singleton);
            builder.Register<SkillStateQueueCreatorService>(Lifetime.Singleton);
            builder.Register<ToIndexService>(Lifetime.Singleton);
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
            builder.Register<SkillEndState>(Lifetime.Singleton);
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
            builder.Register<BuffRegisterService>(Lifetime.Singleton);
            builder.Register<CharacterOutputDataCreatorService>(Lifetime.Singleton);
            builder.Register<CharacterPropertyFactoryService>(Lifetime.Singleton);
            builder.Register<DamageEvaluatorService>(Lifetime.Singleton);
            builder.Register<DamageGeneratorService>(Lifetime.Singleton);
            builder.Register<DamageRegistererService>(Lifetime.Singleton);
            builder.Register<DestroyedPartGeneratorService>(Lifetime.Singleton);
            builder.Register<IsHitEvaluatorService>(Lifetime.Singleton);
            builder.Register<OrderService>(Lifetime.Singleton);
            builder.Register<RestoreGeneratorService>(Lifetime.Singleton);
            builder.Register<SkillExecutorService>(Lifetime.Singleton);
            builder.Register<SlipDamageGeneratorService>(Lifetime.Singleton);
            builder.Register<SlipGeneratorService>(Lifetime.Singleton);
            builder.Register<SlipRegistererService>(Lifetime.Singleton);
            builder.Register<ToBodyPartNumberService>(Lifetime.Singleton);
            builder.Register<ToBuffNumberService>(Lifetime.Singleton);
            builder.Register<TurnInitializerService>(Lifetime.Singleton);
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
        }
    }
}