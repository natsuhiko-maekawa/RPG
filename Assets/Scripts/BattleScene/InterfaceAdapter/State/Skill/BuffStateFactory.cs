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
        private readonly SkillMessageStateFactory _skillMessageStateFactory;

        public BuffStateFactory(
            BattleLoggerService battleLogger,
            BuffDomainService buff,
            BuffGeneratorService buffGenerator,
            SkillMessageStateFactory skillMessageStateFactory)
        {
            _battleLogger = battleLogger;
            _buff = buff;
            _buffGenerator = buffGenerator;
            _skillMessageStateFactory = skillMessageStateFactory;
        }

        public BuffState Create(
            SkillCommonValueObject skillCommon,
            BuffParameterValueObject buffParameter) => new BuffState(
            battleLogger: _battleLogger,
            buff: _buff,
            buffGenerator: _buffGenerator,
            skillMessageStateFactory: _skillMessageStateFactory,
            skillCommon: skillCommon,
            buffParameter: buffParameter);
    }
}