using System;
using BattleScene.Domain.Code;
using BattleScene.UseCase.BusinessLogic;
using BattleScene.UseCase.BusinessLogic.Interface;
using BattleScene.UseCase.Event;
using VContainer;

namespace BattleScene.UseCase.Main
{
    public class UseCaseFactory
    {
        private readonly IObjectResolver _container;

        public UseCaseFactory(IObjectResolver container)
        {
            _container = container;
        }

        public IUseCase Create(UseCaseCode useCaseCode)
        {
            return useCaseCode switch
            {
                UseCaseCode.InitializationLogic => _container.Resolve<InitializationLogic>(),
                UseCaseCode.BattleStartLogic => _container.Resolve<BattleStartLogic>(),
                UseCaseCode.OrderDecisionLogic => _container.Resolve<OrderDecisionLogic>(),
                UseCaseCode.EnemySelectSkillLogic => _container.Resolve<EnemySelectSkillLogic>(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}