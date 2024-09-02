using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class DamageStateFactory
    {
        private readonly BattleLoggerService _battleLogger;
        private readonly DamageGeneratorService _damageGenerator;
        private readonly SkillMessageStateFactory _skillMessageStateFactory;

        public DamageStateFactory(
            BattleLoggerService battleLogger,
            DamageGeneratorService damageGenerator,
            SkillMessageStateFactory skillMessageStateFactory)
        {
            _battleLogger = battleLogger;
            _damageGenerator = damageGenerator;
            _skillMessageStateFactory = skillMessageStateFactory;
        }

        public DamageState Create(
            SkillCommonValueObject skillCommon,
            DamageParameterValueObject damageParameter) => new DamageState(
            battleLogger: _battleLogger,
            damageGenerator: _damageGenerator,
            skillMessageStateFactory: _skillMessageStateFactory,
            skillCommon: skillCommon,
            damageParameter: damageParameter);
    }
}