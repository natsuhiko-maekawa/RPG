using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.UseCases.StateMachine.SkillStack;
using VContainer;

namespace BattleScene.UseCases.StateMachine
{
    public class SkillStateFactory
    {
        private readonly IObjectResolver _container;
        
        public SkillStateFactory(
            IObjectResolver container)
        {
            _container = container;
        }

        public SkillState Create(SkillCode skillCode) => new SkillState(skillCode, _container);
    }
}