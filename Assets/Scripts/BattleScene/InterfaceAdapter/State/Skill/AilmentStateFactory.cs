using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class AilmentStateFactory
    {
        private readonly AilmentGeneratorService _ailmentGenerator;
        private readonly AilmentRegistererService _ailmentRegisterer;
        private readonly BattleLoggerService _battleLoggerService;
        private readonly AilmentMessageState _ailmentMessageState;
        private readonly AilmentFailureState _ailmentFailureState;

        public AilmentStateFactory(
            AilmentGeneratorService ailmentGenerator,
            AilmentRegistererService ailmentRegisterer,
            BattleLoggerService battleLoggerService,
            AilmentMessageState ailmentMessageState,
            AilmentFailureState ailmentFailureState)
        {
            _ailmentGenerator = ailmentGenerator;
            _ailmentRegisterer = ailmentRegisterer;
            _battleLoggerService = battleLoggerService;
            _ailmentMessageState = ailmentMessageState;
            _ailmentFailureState = ailmentFailureState;
        }

        public AilmentState Create(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<AilmentParameterValueObject> ailmentParameterList,
            IList<CharacterId> targetIdList)
        {
            var ailmentList = _ailmentGenerator.Generate(
                skillCommon: skillCommon,
                ailmentParameterList: ailmentParameterList,
                targetIdList: targetIdList);
            return new AilmentState(
                ailmentList: ailmentList,
                ailmentRegisterer: _ailmentRegisterer,
                battleLoggerService: _battleLoggerService,
                ailmentMessageState: _ailmentMessageState,
                ailmentFailureState: _ailmentFailureState);
        }
    }
}