using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.UseCases.StateMachine.SkillStateMachine
{
    public class DamageStateFactory
    {
        private readonly BattleLoggerService _battleLogger;
        private readonly DamageGeneratorService _damageGenerator;

        public DamageStateFactory(
            BattleLoggerService battleLogger,
            DamageGeneratorService damageGenerator)
        {
            _battleLogger = battleLogger;
            _damageGenerator = damageGenerator;
        }

        public DamageState Create(
            SkillCommonValueObject skillCommon,
            DamageParameterValueObject damageParameter) => new DamageState(
            battleLogger: _battleLogger,
            damageGenerator: _damageGenerator,
            skillCommon: skillCommon,
            damageParameter: damageParameter);
    }
}