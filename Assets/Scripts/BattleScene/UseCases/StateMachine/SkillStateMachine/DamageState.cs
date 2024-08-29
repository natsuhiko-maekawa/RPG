using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.UseCases.StateMachine.SkillStateMachine
{
    public class DamageState : AbstractSkillState
    {
        private readonly DamageGeneratorService _damageGenerator;
        private readonly SkillCommonValueObject _skillCommon;
        private readonly DamageParameterValueObject _damageParameter;
        
        public DamageState(
            SkillCommonValueObject skillCommon,
            DamageParameterValueObject damageParameter)
        {
            _skillCommon = skillCommon;
            _damageParameter = damageParameter;
        }

        public override void Start()
        {
            var damage = _damageGenerator.Generate(
                skillCommon: _skillCommon,
                damageParameter: _damageParameter);
        }
    }
}