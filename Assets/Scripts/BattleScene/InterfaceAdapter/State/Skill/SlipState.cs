using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class SlipState : AbstractSkillState<SlipParameterValueObject, SlipValueObject>
    {
        private readonly BattleLogDomainService _battleLog;
        private readonly BattleLoggerService _battleLogger;
        private readonly SlipGeneratorService _slipGenerator;
        private readonly SlipRegistererService _slipRegisterer;
        private readonly SlipMessageState _slipMessageState;
        private readonly SkillCommonValueObject _skillCommon;
        private readonly SlipParameterValueObject _slipParameter;
        private readonly ImmutableList<CharacterId> _targetIdList;

        public SlipState(
            BattleLogDomainService battleLog,
            BattleLoggerService battleLogger,
            SlipGeneratorService slipGenerator,
            SlipRegistererService slipRegisterer,
            SlipMessageState slipMessageState,
            SkillCommonValueObject skillCommon,
            SlipParameterValueObject slipParameter,
            IReadOnlyList<CharacterId> targetIdList)
        {
            _battleLog = battleLog;
            _battleLogger = battleLogger;
            _slipGenerator = slipGenerator;
            _slipRegisterer = slipRegisterer;
            _slipMessageState = slipMessageState;
            _skillCommon = skillCommon;
            _slipParameter = slipParameter;
            _targetIdList = targetIdList.ToImmutableList();
        }
        
        public override void Start()
        {
            var slip = _slipGenerator.Generate(
                skillCommon: _skillCommon,
                slipParameter: _slipParameter,
                targetIdList: _targetIdList);
            _slipRegisterer.Register(slip);
            _battleLogger.Log(slip);
            SkillContext.TransitionTo(_slipMessageState);
        }
    }
}