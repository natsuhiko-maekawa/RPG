using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class BuffStateFactory
    {
        private readonly BattleLoggerService _battleLogger;
        private readonly BuffGeneratorService _buffGenerator;

        public BuffStateFactory(
            BattleLoggerService battleLogger,
            BuffGeneratorService buffGenerator)
        {
            _battleLogger = battleLogger;
            _buffGenerator = buffGenerator;
        }

        public BuffState Create(
            SkillCommonValueObject skillCommon,
            BuffParameterValueObject buffParameter) => new BuffState(
            battleLogger: _battleLogger,
            buffGenerator: _buffGenerator,
            skillCommon: skillCommon,
            buffParameter: buffParameter);
    }
}