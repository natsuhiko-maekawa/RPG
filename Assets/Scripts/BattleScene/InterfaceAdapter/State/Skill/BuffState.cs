using BattleScene.Domain.DomainService;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class BuffState : AbstractSkillState
    {
        private readonly BattleLoggerService _battleLogger;
        private readonly BuffDomainService _buff;
        private readonly BuffGeneratorService _buffGenerator;
        private readonly BuffMessageState _buffMessageState;
        private readonly SkillCommonValueObject _skillCommon;
        private readonly BuffParameterValueObject _buffParameter;

        public BuffState(
            BattleLoggerService battleLogger,
            BuffDomainService buff,
            BuffGeneratorService buffGenerator,
            BuffMessageState buffMessageState,
            SkillCommonValueObject skillCommon,
            BuffParameterValueObject buffParameter)
        {
            _battleLogger = battleLogger;
            _buff = buff;
            _buffGenerator = buffGenerator;
            _buffMessageState = buffMessageState;
            _skillCommon = skillCommon;
            _buffParameter = buffParameter;
        }

        public override void Start()
        {
            var buff = _buffGenerator.Generate(
                skillCommon: _skillCommon,
                buffParameter: _buffParameter);
            _buff.Add(buff);
            _battleLogger.Log(buff);
            SkillContext.TransitionTo(_buffMessageState);
        }
    }
}