using BattleScene.Domain.Code;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.StateMachine.SkillStateMachine;
using VContainer;

namespace BattleScene.UseCases.StateMachine
{
    public class SkillStateFactory
    {
        private readonly IObjectResolver _container;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        
        public SkillStateFactory(
            IObjectResolver container,
            IFactory<SkillValueObject, SkillCode> skillFactory)
        {
            _container = container;
            _skillFactory = skillFactory;
        }

        public SkillState Create(SkillCode skillCode) => new SkillState(skillCode, _container, _skillFactory);
    }
}