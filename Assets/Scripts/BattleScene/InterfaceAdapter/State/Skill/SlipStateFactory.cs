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
        private readonly SlipRegistererService _slipRegisterer;
        private readonly SlipFailureState _slipFailureState;
        private readonly SlipMessageState _slipMessageState;

        public SlipStateFactory(
            BattleLogDomainService battleLog,
            BattleLoggerService battleLogger,
            SlipGeneratorService slipGenerator,
            SlipRegistererService slipRegisterer,
            SlipFailureState slipFailureState,
            SlipMessageState slipMessageState)
        {
            _battleLog = battleLog;
            _battleLogger = battleLogger;
            _slipGenerator = slipGenerator;
            _slipRegisterer = slipRegisterer;
            _slipFailureState = slipFailureState;
            _slipMessageState = slipMessageState;
        }

        public SlipState Create(
            SkillCommonValueObject skillCommon,
            SlipParameterValueObject slipParameter,
            IList<CharacterId> targetIdList) => new(
            battleLog: _battleLog,
            battleLogger: _battleLogger,
            slipGenerator: _slipGenerator,
            slipRegisterer: _slipRegisterer,
            slipFailureState: _slipFailureState,
            slipMessageState: _slipMessageState,
            skillCommon: skillCommon,
            slipParameter: slipParameter,
            targetIdList: targetIdList);
    }
}