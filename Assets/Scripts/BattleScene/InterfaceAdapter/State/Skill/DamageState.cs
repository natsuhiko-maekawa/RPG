using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class DamageState : AbstractSkillState
    {
        private readonly DamageGeneratorService _damageGenerator;
        private readonly BattleLoggerService _battleLogger;
        private readonly DamageMessageState _damageMessageState;
        private readonly SkillCommonValueObject _skillCommon;
        private readonly DamageParameterValueObject _damageParameter;
        private readonly ImmutableList<CharacterId> _targetIdList;
        
        public DamageState(
            BattleLoggerService battleLogger,
            DamageGeneratorService damageGenerator,
            DamageMessageState damageMessageState,
            SkillCommonValueObject skillCommon,
            DamageParameterValueObject damageParameter,
            IList<CharacterId> targetIdList)
        {
            _battleLogger = battleLogger;
            _damageGenerator = damageGenerator;
            _damageMessageState = damageMessageState;
            _skillCommon = skillCommon;
            _damageParameter = damageParameter;
            _targetIdList = targetIdList.ToImmutableList();
        }

        public override void Start()
        {
            var damage = _damageGenerator.Generate(
                skillCommon: _skillCommon,
                damageParameter: _damageParameter,
                targetIdList: _targetIdList);
            _battleLogger.Log(damage);
            SkillContext.TransitionTo(_damageMessageState);
        }
    }
}