using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class DamageStateFactory
    {
        private readonly BattleLoggerService _battleLogger;
        private readonly DamageGeneratorService _damageGenerator;
        private readonly DamageMessageState _damageMessageState;

        public DamageStateFactory(
            BattleLoggerService battleLogger,
            DamageGeneratorService damageGenerator,
            DamageMessageState damageMessageState)
        {
            _battleLogger = battleLogger;
            _damageGenerator = damageGenerator;
            _damageMessageState = damageMessageState;
        }

        public DamageState Create(
            SkillCommonValueObject skillCommon,
            DamageParameterValueObject damageParameter) => new DamageState(
            battleLogger: _battleLogger,
            damageGenerator: _damageGenerator,
            damageMessageState: _damageMessageState,
            skillCommon: skillCommon,
            damageParameter: damageParameter);
    }
}