using System.Collections.Generic;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class AilmentStateFactory
    {
        private readonly AilmentGeneratorService _ailmentGenerator;
        private readonly BattleLoggerService _battleLoggerService;
        private readonly BattleLogDomainService _battleLog;
        private readonly AilmentMessageState _ailmentMessageState;
        private readonly AilmentFailureState _ailmentFailureState;

        public AilmentStateFactory(
            AilmentGeneratorService ailmentGenerator,
            BattleLoggerService battleLoggerService,
            BattleLogDomainService battleLog,
            AilmentMessageState ailmentMessageState,
            AilmentFailureState ailmentFailureState)
        {
            _ailmentGenerator = ailmentGenerator;
            _battleLoggerService = battleLoggerService;
            _battleLog = battleLog;
            _ailmentMessageState = ailmentMessageState;
            _ailmentFailureState = ailmentFailureState;
        }

        public AilmentState Create(
            SkillCommonValueObject skillCommon,
            AilmentParameterValueObject ailmentParameter,
            IList<CharacterId> targetIdList) => new AilmentState(
            ailmentGenerator: _ailmentGenerator,
            battleLoggerService: _battleLoggerService,
            battleLog: _battleLog,
            skillCommon: skillCommon,
            ailmentParameter: ailmentParameter,
            targetIdList: targetIdList,
            ailmentMessageState: _ailmentMessageState,
            ailmentFailureState: _ailmentFailureState);
    }
}