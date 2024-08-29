using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;
using VContainer;

namespace BattleScene.UseCases.StateMachine.SkillStateMachine
{
    public class DamageState : AbstractSkillState
    {
        private readonly DamageGeneratorService _damageGenerator;
        private readonly SkillCommonValueObject _skillCommon;
        private readonly DamageParameterValueObject _damageParameter;
        private readonly BattleLoggerService _battleLogger;
        private readonly IObjectResolver _container;
        
        public DamageState(
            BattleLoggerService battleLogger,
            DamageGeneratorService damageGenerator,
            IObjectResolver container,
            SkillCommonValueObject skillCommon,
            DamageParameterValueObject damageParameter)
        {
            _battleLogger = battleLogger;
            _damageGenerator = damageGenerator;
            _container = container;
            _skillCommon = skillCommon;
            _damageParameter = damageParameter;
        }

        public override void Start()
        {
            var damage = _damageGenerator.Generate(
                skillCommon: _skillCommon,
                damageParameter: _damageParameter);
            _battleLogger.Log(damage);
            SkillContext.TransitionTo(_container.Resolve<DamageMessageState>());
        }
    }
}