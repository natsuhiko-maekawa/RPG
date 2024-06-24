using System;
using System.Collections.Generic;
using BattleScene.UseCases.Output.Interface;
using BattleScene.UseCases.UseCase;
using BattleScene.UseCases.UseCase.Interface;
using VContainer;

namespace BattleScene.UseCases.StateMachine.Service
{
    internal class EnemySkillStateCreatorService
    {
        private readonly IObjectResolver _container;

        public EnemySkillStateCreatorService(
            IObjectResolver container)
        {
            _container = container;
        }
        
        public State Create()
        {
            var useCaseList = new List<IUseCase>
            {
                _container.Resolve<EnemySelectSkill>(),
            };

            var outputList = new List<IOutput>
            {
            };

            var startEvent = new State.Event(useCaseList, outputList);

            var triggerDict = new Dictionary<Func<bool>, StateCode>
            {
            };

            return new State(
                triggerDict: triggerDict,
                startEvent: startEvent);
        }
    }
}