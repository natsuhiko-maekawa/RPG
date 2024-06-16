using System;
using BattleScene.Domain.Code;
using BattleScene.UseCase.BusinessLogic;
using BattleScene.UseCase.BusinessLogic.Interface;
using BattleScene.UseCase.Event;
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
                BusinessLogicCode.EnemySelectSkillLogic => _container.Resolve<EnemySelectSkillLogic>(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}