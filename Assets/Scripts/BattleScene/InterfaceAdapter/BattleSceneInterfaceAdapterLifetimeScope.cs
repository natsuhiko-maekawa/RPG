using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Controller;
using BattleScene.InterfaceAdapter.DataAccess.Factory;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using BattleScene.InterfaceAdapter.DataAccess.Repository;
using BattleScene.InterfaceAdapter.Presenter.AilmentsView;
using BattleScene.InterfaceAdapter.Presenter.BuffView;
using BattleScene.InterfaceAdapter.Presenter.CharacterVibesView;
using BattleScene.InterfaceAdapter.Presenter.DestroyedPartView;
using BattleScene.InterfaceAdapter.Presenter.DigitView;
using BattleScene.InterfaceAdapter.Presenter.EnemyView;
using BattleScene.InterfaceAdapter.Presenter.FrameView;
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
using BattleScene.UseCases.IController;
using BattleScene.UseCases.IPresenter;
using BattleScene.UseCases.View.AilmentView.OutputBoundary;
using BattleScene.UseCases.View.AttackCountView.OutputBoundary;
using BattleScene.UseCases.View.BuffView.OutputBoundary;
using BattleScene.UseCases.View.CharacterVibesView.OutputBoundary;
using BattleScene.UseCases.View.DestroyedPartView.OutputBoundary;
using BattleScene.UseCases.View.DigitView.OutputBoundary;
using BattleScene.UseCases.View.EnemyView.OutputBoundary;
using BattleScene.UseCases.View.FrameView.OutputBoundary;
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
    public class BattleSceneInterfaceAdapterLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
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
            builder.Register<IBattleSceneController, BattleSceneController>(Lifetime.Singleton);
            
            // builder.Register<IActionTimeRepository, ActionTimeRepository>(Lifetime.Singleton);
            builder.Register<IAilmentRepository, AilmentRepository>(Lifetime.Singleton);
            builder.Register<IBodyPartRepository, BodyPartRepository>(Lifetime.Singleton);
            // builder.Register<IBuffRepository, BuffRepository>(Lifetime.Singleton);
            builder.Register<ICharacterRepository, CharacterRepository>(Lifetime.Singleton);
            builder.Register<IEnemyRepository, EnemyRepository>(Lifetime.Singleton);
            builder.Register<IFrameRepository, FrameRepository>(Lifetime.Singleton);
            // builder.Register<IHitPointRepository, HitPointRepository>(Lifetime.Singleton);
            // builder.Register<IOrderRepository, OrderRepository>(Lifetime.Singleton);
            builder.Register<IResultRepository, ResultRepository>(Lifetime.Singleton);
            builder.Register<ISelectorRepository, SelectorRepository>(Lifetime.Singleton);
            builder.Register<ISkillRepository, SkillRepository>(Lifetime.Singleton);
            builder.Register<ISkillSelectorRepository, SkillSelectorRepository>(Lifetime.Singleton);
            // builder.Register<ISlipDamageRepository, SlipDamageRepository>(Lifetime.Singleton);
            builder.Register<ITargetRepository, TargetRepository>(Lifetime.Singleton);
            // builder.Register<ITechnicalPointRepository, TechnicalPointRepository>(Lifetime.Singleton);
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
                .Register<IRepository<TechnicalPointAggregate, CharacterId>,
                    Repository<TechnicalPointAggregate, CharacterId>>(Lifetime.Singleton);
            builder.Register<IRepository<TurnEntity, TurnId>, Repository<TurnEntity, TurnId>>(Lifetime.Singleton);
            builder.Register<IFactory<SkillValueObject, SkillCode>, SkillFactory>(Lifetime.Singleton);
            builder.Register<IFactory<SkillViewInfoDto, SkillCode>, SkillViewInfoListScriptableObjectFactory>
                (Lifetime.Singleton);

            builder.Register<IAilmentFactory, AilmentFactory>(Lifetime.Singleton);
            builder.Register<IAilmentViewInfoFactory, AilmentViewInfoFactory>(Lifetime.Singleton);
            builder.Register<IFactory<AilmentViewInfoDto, AilmentCode>, AilmentViewInfoListScriptableObjectFactory>(
                Lifetime.Singleton);
            builder.Register<IBodyPartFactory, BodyPartFactory>(Lifetime.Singleton);
            builder.Register<IFactory<BodyPartViewInfoDto, BodyPartCode>, BodyPartViewInfoFactory>(Lifetime.Singleton);
            builder.Register<IEnemyViewInfoFactory, EnemyViewInfoFactory>(Lifetime.Singleton);
            builder.Register<IFactory<MessageDto, MessageCode>, MessageFactory>(Lifetime.Singleton);
            builder.Register<IPlayerPropertyFactory, PlayerPropertyFactory>(Lifetime.Singleton);
            builder.Register<IFactory<PlayerViewInfoDto, CharacterTypeId>, PlayerViewInfoFactory>(Lifetime.Singleton);
            builder.Register<IPropertyFactory, PropertyFactory>(Lifetime.Singleton);
            builder.Register<ISlipDamageFactory, SlipDamageFactory>(Lifetime.Singleton);

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
            
            // TODO: DomainのServiceを登録している
            builder.Register<OrderedItemsDomainService>(Lifetime.Singleton);
            builder.Register<ResultDomainService>(Lifetime.Singleton);
        }
    }
}