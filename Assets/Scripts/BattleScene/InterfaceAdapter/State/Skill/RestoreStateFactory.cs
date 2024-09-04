using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class RestoreStateFactory
    {
        private readonly BattleLoggerService _battleLogger;
        private readonly RestoreGeneratorService _restoreGenerator;
        private readonly RestoreMessageState _restoreMessageState;

        public RestoreStateFactory(
            BattleLoggerService battleLogger,
            RestoreGeneratorService restoreGenerator,
            RestoreMessageState restoreMessageState)
        {
            _battleLogger = battleLogger;
            _restoreGenerator = restoreGenerator;
            _restoreMessageState = restoreMessageState;
        }

        public RestoreState Create(
            SkillCommonValueObject skillCommon,
            RestoreParameterValueObject restoreParameter) => new RestoreState(
            skillCommon: skillCommon,
            restoreParameter: restoreParameter,
            battleLogger: _battleLogger,
            restoreGenerator: _restoreGenerator,
            restoreMessageState: _restoreMessageState);
    }
}