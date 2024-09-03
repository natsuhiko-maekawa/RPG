using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class AilmentState : AbstractSkillState
    {
        private readonly AilmentParameterValueObject _ailmentParameter;
        private readonly AilmentSkillService _ailmentSkill;
        
        public AilmentState(
            AilmentParameterValueObject ailmentParameter)
        {
            
        }
    }
}