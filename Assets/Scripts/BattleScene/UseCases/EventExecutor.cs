using System;
using BattleScene.UseCases.Event;
using BattleScene.UseCases.StateMachine;
using VContainer;

namespace BattleScene.UseCases
{
    public class EventExecutor
    {
        private readonly IObjectResolver _container;

        public EventExecutor(IObjectResolver container)
        {
            _container = container;
        }

        public StateCode Execute(StateCode stateCode)
        {
            return stateCode switch
            {
                StateCode.Initialize => _container.Resolve<InitializationEvent>().Execute(),
                StateCode.InitializeEnemy => _container.Resolve<EnemyInitializerEvent>().Execute(),
                StateCode.Order => _container.Resolve<OrderEvent>().Execute(),
                StateCode.EnemySkill => _container.Resolve<EnemySkillSelectorEvent>().Execute(),
                StateCode.ExecuteSkill => _container.Resolve<SkillExecutorEvent>().Execute(),
                _ => throw new ArgumentOutOfRangeException(nameof(stateCode), stateCode, null)
            };
        }
    }
}