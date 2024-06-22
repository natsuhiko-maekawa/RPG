using System;
using System.Collections.Generic;
using BattleScene.UseCase.BusinessLogic;
using BattleScene.UseCase.BusinessLogic.Interface;
using BattleScene.UseCase.Output.Interface;
using VContainer;

namespace BattleScene.UseCase.Main
{
    internal class EnemySkillStateFactory
    {
        private readonly IObjectResolver _container;

        public EnemySkillStateFactory(
            IObjectResolver container)
        {
            _container = container;
        }
        
        public State Create()
        {
            var useCaseList = new List<IUseCase>
            {
                _container.Resolve<EnemySelectSkillLogic>(),
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