using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.UseCases.StateMachine.SkillStateMachine
{
    public class DamageStateFactory
    {
        private readonly DamageGeneratorService _damageGenerator;

        public DamageStateFactory(
            DamageGeneratorService damageGenerator)
        {
            _damageGenerator = damageGenerator;
        }

        public DamageState Create(
            SkillCommonValueObject skillCommon,
            DamageParameterValueObject damageParameter) => new DamageState(
            skillCommon: skillCommon,
            damageParameter: damageParameter);
    }
}