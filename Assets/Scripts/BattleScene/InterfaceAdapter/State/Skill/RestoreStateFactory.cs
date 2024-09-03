using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class RestoreStateFactory
    {
        private readonly BattleLoggerService _battleLogger;
        private readonly RestoreGeneratorService _restoreGenerator;
        private readonly SkillEndState _skillEndState;

        public RestoreStateFactory(
            BattleLoggerService battleLogger,
            RestoreGeneratorService restoreGenerator)
        {
            _battleLogger = battleLogger;
            _restoreGenerator = restoreGenerator;
        }

        public RestoreState Create(
            SkillCommonValueObject skillCommon,
            RestoreParameterValueObject restoreParameter) => new RestoreState(
            skillCommon: skillCommon,
            restoreParameter: restoreParameter,
            battleLogger: _battleLogger,
            restoreGenerator: _restoreGenerator,
            skillEndState: _skillEndState);
    }
}