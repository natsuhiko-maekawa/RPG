using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class AilmentStateFactory
    {
        private readonly AilmentGeneratorService _ailmentGenerator;
        private readonly BattleLoggerService _battleLoggerService;

        public AilmentStateFactory(
            AilmentGeneratorService ailmentGenerator,
            BattleLoggerService battleLoggerService)
        {
            _ailmentGenerator = ailmentGenerator;
            _battleLoggerService = battleLoggerService;
        }

        public AilmentState Create(
            SkillCommonValueObject skillCommon,
            AilmentParameterValueObject ailmentParameter,
            IList<CharacterId> targetIdList) => new AilmentState(
            ailmentGenerator: _ailmentGenerator,
            battleLoggerService: _battleLoggerService,
            skillCommon: skillCommon,
            ailmentParameter: ailmentParameter,
            targetIdList: targetIdList);
    }
}