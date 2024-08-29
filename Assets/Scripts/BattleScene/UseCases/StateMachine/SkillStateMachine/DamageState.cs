using BattleScene.Domain.Entity;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.UseCases.StateMachine.SkillStateMachine
{
    public class DamageState : AbstractSkillState
    {
        private readonly DamageGeneratorService _damageGenerator;
        private readonly SkillCommonValueObject _skillCommon;
        private readonly DamageParameterValueObject _damageParameter;
        private readonly BattleLoggerService _battleLogger;
        
        public DamageState(
            BattleLoggerService battleLogger,
            DamageGeneratorService damageGenerator,
            SkillCommonValueObject skillCommon,
            DamageParameterValueObject damageParameter)
        {
            _battleLogger = battleLogger;
            _damageGenerator = damageGenerator;
            _skillCommon = skillCommon;
            _damageParameter = damageParameter;
        }

        public override void Start()
        {
            var damage = _damageGenerator.Generate(
                skillCommon: _skillCommon,
                damageParameter: _damageParameter);
            _battleLogger.Log(damage);
        }
    }
}