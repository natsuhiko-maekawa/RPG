using BattleScene.Domain.DomainService;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class BuffStateFactory
    {
        private readonly BattleLoggerService _battleLogger;
        private readonly BuffDomainService _buff;
        private readonly BuffGeneratorService _buffGenerator;
        private readonly BuffMessageState _buffMessageState;

        public BuffStateFactory(
            BattleLoggerService battleLogger,
            BuffDomainService buff,
            BuffGeneratorService buffGenerator,
            BuffMessageState buffMessageState)
        {
            _battleLogger = battleLogger;
            _buff = buff;
            _buffGenerator = buffGenerator;
            _buffMessageState = buffMessageState;
        }

        public BuffState Create(
            SkillCommonValueObject skillCommon,
            BuffParameterValueObject buffParameter) => new BuffState(
            battleLogger: _battleLogger,
            buff: _buff,
            buffGenerator: _buffGenerator,
            buffMessageState: _buffMessageState,
            skillCommon: skillCommon,
            buffParameter: buffParameter);
    }
}