using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class DamageState : AbstractSkillState
    {
        private readonly DamageGeneratorService _damageGenerator;
        private readonly SkillCommonValueObject _skillCommon;
        private readonly DamageParameterValueObject _damageParameter;
        private readonly BattleLoggerService _battleLogger;
        private readonly DamageMessageState _damageMessageState;
        
        public DamageState(
            BattleLoggerService battleLogger,
            DamageGeneratorService damageGenerator,
            DamageMessageState damageMessageState,
            SkillCommonValueObject skillCommon,
            DamageParameterValueObject damageParameter)
        {
            _battleLogger = battleLogger;
            _damageGenerator = damageGenerator;
            _damageMessageState = damageMessageState;
            _skillCommon = skillCommon;
            _damageParameter = damageParameter;
        }

        public override void Start()
        {
            var damage = _damageGenerator.Generate(
                skillCommon: _skillCommon,
                damageParameter: _damageParameter);
            _battleLogger.Log(damage);
            SkillContext.TransitionTo(_damageMessageState);
        }
    }
}