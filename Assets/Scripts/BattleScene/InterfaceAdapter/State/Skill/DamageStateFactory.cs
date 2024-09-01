using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;
using VContainer;

namespace BattleScene.UseCases.State.Skill
{
    public class DamageStateFactory
    {
        private readonly BattleLoggerService _battleLogger;
        private readonly DamageGeneratorService _damageGenerator;
        private readonly IObjectResolver _container;

        public DamageStateFactory(
            BattleLoggerService battleLogger,
            DamageGeneratorService damageGenerator,
            IObjectResolver container)
        {
            _battleLogger = battleLogger;
            _damageGenerator = damageGenerator;
            _container = container;
        }

        public DamageState Create(
            SkillCommonValueObject skillCommon,
            DamageParameterValueObject damageParameter) => new DamageState(
            battleLogger: _battleLogger,
            damageGenerator: _damageGenerator,
            container: _container,
            skillCommon: skillCommon,
            damageParameter: damageParameter);
    }
}