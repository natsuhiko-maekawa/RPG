using System.Collections.Generic;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class SlipStateFactory
    {
        private readonly BattleLogDomainService _battleLog;
        private readonly BattleLoggerService _battleLogger;
        private readonly SlipGeneratorService _slipGenerator;
        private readonly SlipFailureState _slipFailureState;
        private readonly SlipMessageState _slipMessageState;

        public SlipStateFactory(
            BattleLogDomainService battleLog,
            BattleLoggerService battleLogger,
            SlipGeneratorService slipGenerator,
            SlipFailureState slipFailureState,
            SlipMessageState slipMessageState)
        {
            _battleLog = battleLog;
            _battleLogger = battleLogger;
            _slipGenerator = slipGenerator;
            _slipFailureState = slipFailureState;
            _slipMessageState = slipMessageState;
        }

        public SlipState Create(
            SkillCommonValueObject skillCommon,
            SlipParameterValueObject slipParameter,
            IList<CharacterId> targetIdList) => new(
            battleLog: _battleLog,
            battleLogger: _battleLogger,
            slipDamageGenerator: _slipGenerator,
            slipFailureState: _slipFailureState,
            slipMessageState: _slipMessageState,
            skillCommon: skillCommon,
            slipParameter: slipParameter,
            targetIdList: targetIdList);
    }
}