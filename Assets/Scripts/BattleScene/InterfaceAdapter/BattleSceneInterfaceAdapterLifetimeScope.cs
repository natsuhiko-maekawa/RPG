﻿using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.InterfaceAdapter.Controller;
using BattleScene.InterfaceAdapter.DataAccess.Factory;
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
using BattleScene.UseCases.IController;
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
using BattleScene.UseCases.View.OrderView.OutputBoundary;
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
            builder.Register<ITechnicalPointRepository, TechnicalPointRepository>(Lifetime.Singleton);
            builder.Register<IRepository<ActionTimeEntity, CharacterId>, Repository<ActionTimeEntity, CharacterId>>(
                Lifetime.Singleton);
            builder.Register<IRepository<BuffEntity, BuffId>, Repository<BuffEntity, BuffId>>(
                Lifetime.Singleton);
            builder.Register<IRepository<HitPointAggregate, CharacterId>, Repository<HitPointAggregate, CharacterId>>(
                Lifetime.Singleton);
            builder.Register<IRepository<OrderedItemEntity, OrderNumber>, Repository<OrderedItemEntity, OrderNumber>>(
                Lifetime.Singleton);
            builder.Register<IRepository<SlipDamageEntity, SlipDamageId>, Repository<SlipDamageEntity, SlipDamageId>>(
                Lifetime.Singleton);

            builder.Register<IAilmentFactory, AilmentFactory>(Lifetime.Singleton);
            builder.Register<IAilmentViewInfoFactory, AilmentViewInfoFactory>(Lifetime.Singleton);
            builder.Register<IBodyPartFactory, BodyPartFactory>(Lifetime.Singleton);
            builder.Register<IEnemyViewInfoFactory, EnemyViewInfoFactory>(Lifetime.Singleton);
            builder.Register<IMessageFactory, MessageFactory>(Lifetime.Singleton);
            builder.Register<IPlayerPropertyFactory, PlayerPropertyFactory>(Lifetime.Singleton);
            builder.Register<IPropertyFactory, PropertyFactory>(Lifetime.Singleton);
            builder.Register<ISlipDamageFactory, SlipDamageFactory>(Lifetime.Singleton);

            builder.Register<ToAilmentNumberService>(Lifetime.Singleton);
        }
    }
}