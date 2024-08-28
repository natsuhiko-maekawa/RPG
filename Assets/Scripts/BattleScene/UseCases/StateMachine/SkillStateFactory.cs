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

        public SkillState Create(SkillEntity skill) => new SkillState(skill, _container);
    }
}