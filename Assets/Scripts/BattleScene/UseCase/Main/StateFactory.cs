using System;
using VContainer;

namespace BattleScene.UseCase.Main
{
    internal class StateFactory
    {
        private readonly IObjectResolver _container;

        public StateFactory(
            IObjectResolver container)
        {
            _container = container;
        }
        
        public State Create(StateCode stateCode)
        {
            return stateCode switch
            {
                StateCode.Initialize => _container.Resolve<InitializeStateFactory>().Create(),
                StateCode.EnemySkill => _container.Resolve<EnemySkillStateFactory>().Create(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}