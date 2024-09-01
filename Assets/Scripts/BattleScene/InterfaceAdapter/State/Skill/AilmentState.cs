using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class AilmentState : AbstractSkillState
    {
        private readonly AilmentValueObject _ailment;
        private readonly AilmentSkillService _ailmentSkill;
        
        public AilmentState(
            AilmentValueObject ailment)
        {
            
        }
    }
}