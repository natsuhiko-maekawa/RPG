using System;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Output.Interface;
using BattleScene.UseCases.View.AilmentView;
using BattleScene.UseCases.View.OrderView;
using VContainer;

namespace BattleScene.UseCases.Main
{
    public class OutputFactory
    {
        private readonly IObjectResolver _container;

        public OutputFactory(IObjectResolver container)
        {
            _container = container;
        }

        public IOutput Create(OutputCode outputCode)
        {
            return outputCode switch
            {
                OutputCode.AilmentView => _container.Resolve<AilmentViewOutput>(),
                OutputCode.Order => _container.Resolve<OrderViewOutput>(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}