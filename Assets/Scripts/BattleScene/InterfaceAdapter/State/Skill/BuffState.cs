using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class BuffState : AbstractSkillState
    {
        private readonly BattleLoggerService _battleLogger;
        private readonly BuffGeneratorService _buffGenerator;
        private readonly SkillCommonValueObject _skillCommon;
        private readonly BuffParameterValueObject _buffParameter;

        public BuffState(
            BattleLoggerService battleLogger,
            BuffGeneratorService buffGenerator,
            SkillCommonValueObject skillCommon,
            BuffParameterValueObject buffParameter)
        {
            _battleLogger = battleLogger;
            _buffGenerator = buffGenerator;
            _skillCommon = skillCommon;
            _buffParameter = buffParameter;
        }

        public override void Start()
        {
            var buff = _buffGenerator.Generate(
                skillCommon: _skillCommon,
                buffParameter: _buffParameter);
            _battleLogger.Log(buff);
        }
    }
}