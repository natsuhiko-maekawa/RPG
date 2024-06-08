using System;
using BattleScene.Domain.Code;
using BattleScene.UseCase.Output.Interface;
using BattleScene.UseCase.View.AilmentView;
using BattleScene.UseCase.View.OrderView;
using VContainer;

namespace BattleScene.UseCase.Main
{
    public class OutputFactory
    {
        private readonly IObjectResolver _container;

        public OutputFactory(IObjectResolver container)
        {
            _container = container;
        }

        public IOutput Create(PresenterCode presenterCode)
        {
            return presenterCode switch
            {
                PresenterCode.AilmentView => _container.Resolve<AilmentViewOutput>(),
                PresenterCode.Order => _container.Resolve<OrderViewOutput>(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}