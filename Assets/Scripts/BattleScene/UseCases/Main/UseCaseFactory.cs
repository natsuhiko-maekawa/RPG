using System;
using BattleScene.Domain.Code;
using BattleScene.UseCases.UseCase;
using BattleScene.UseCases.UseCase.Interface;
using VContainer;

namespace BattleScene.UseCases.Main
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
                UseCaseCode.InitializationLogic => _container.Resolve<Initialization>(),
                UseCaseCode.BattleStartLogic => _container.Resolve<BattleStart>(),
                UseCaseCode.OrderDecisionLogic => _container.Resolve<OrderDecision>(),
                UseCaseCode.EnemySelectSkillLogic => _container.Resolve<EnemySelectSkill>(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}