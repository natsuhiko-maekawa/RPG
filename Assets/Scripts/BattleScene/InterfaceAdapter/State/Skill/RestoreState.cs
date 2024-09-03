using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class RestoreState : AbstractSkillState
    {
        private readonly BattleLoggerService _battleLogger;
        private readonly RestoreGeneratorService _restoreGenerator;
        private readonly SkillCommonValueObject _skillCommon;
        private readonly RestoreParameterValueObject _restoreParameter;

        public RestoreState(
            SkillCommonValueObject skillCommon,
            RestoreParameterValueObject restoreParameter,
            BattleLoggerService battleLogger,
            RestoreGeneratorService restoreGenerator)
        {
            _skillCommon = skillCommon;
            _restoreParameter = restoreParameter;
            _battleLogger = battleLogger;
            _restoreGenerator = restoreGenerator;
        }

        public override void Start()
        {
            var restore = _restoreGenerator.Generate(
                skillCommon: _skillCommon,
                restoreParameter: _restoreParameter);
            _battleLogger.Log(restore);
        }
    }
}