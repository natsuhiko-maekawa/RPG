using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class SlipState : AbstractSkillState
    {
        private readonly BattleLogDomainService _battleLog;
        private readonly BattleLoggerService _battleLogger;
        private readonly SlipGeneratorService _slipDamageGenerator;
        private readonly SlipFailureState _slipFailureState;
        private readonly SlipMessageState _slipMessageState;
        private readonly SkillCommonValueObject _skillCommon;
        private readonly SlipParameterValueObject _slipParameter;
        private readonly ImmutableList<CharacterId> _targetIdList;

        public SlipState(
            BattleLogDomainService battleLog,
            BattleLoggerService battleLogger,
            SlipGeneratorService slipDamageGenerator,
            SlipFailureState slipFailureState,
            SlipMessageState slipMessageState,
            SkillCommonValueObject skillCommon,
            SlipParameterValueObject slipParameter,
            IList<CharacterId> targetIdList)
        {
            _battleLog = battleLog;
            _battleLogger = battleLogger;
            _slipDamageGenerator = slipDamageGenerator;
            _slipFailureState = slipFailureState;
            _slipMessageState = slipMessageState;
            _skillCommon = skillCommon;
            _slipParameter = slipParameter;
            _targetIdList = targetIdList.ToImmutableList();
        }
        
        public override void Start()
        {
            var slip = _slipDamageGenerator.Generate(
                skillCommon: _skillCommon,
                slipParameter: _slipParameter,
                targetIdList: _targetIdList);
            _battleLogger.Log(slip);
            AbstractSkillState nextState = _battleLog.GetLast().ActualTargetIdList.IsEmpty
                ? _slipFailureState 
                : _slipMessageState;
            SkillContext.TransitionTo(nextState);
        }
    }
}