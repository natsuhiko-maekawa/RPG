using System;
using BattleScene.Domain.Code;
using BattleScene.UseCase.BusinessLogic;
using BattleScene.UseCase.BusinessLogic.Interface;
using VContainer;

namespace BattleScene.UseCase.Main
{
    public class BusinessLogicFactory
    {
        private readonly IObjectResolver _container;

        public BusinessLogicFactory(IObjectResolver container)
        {
            _container = container;
        }

        public IBusinessLogic Create(BusinessLogicCode businessLogicCode)
        {
            return businessLogicCode switch
            {
                BusinessLogicCode.InitializationLogic => _container.Resolve<InitializationLogic>(),
                BusinessLogicCode.BattleStartLogic => _container.Resolve<BattleStartLogic>(),
                BusinessLogicCode.OrderDecisionLogic => _container.Resolve<OrderDecisionLogic>(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}