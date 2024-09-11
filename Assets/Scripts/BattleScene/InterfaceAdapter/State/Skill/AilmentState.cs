using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class AilmentState : AbstractSkillState
    {
        private readonly AilmentGeneratorService _ailmentGenerator;
        private readonly BattleLoggerService _battleLoggerService;
        private readonly SkillCommonValueObject _skillCommon;
        private readonly AilmentParameterValueObject _ailmentParameter;
        private readonly ImmutableList<CharacterId> _targetIdList;

        public AilmentState(
            AilmentGeneratorService ailmentGenerator, 
            BattleLoggerService battleLoggerService,
            SkillCommonValueObject skillCommon,
            AilmentParameterValueObject ailmentParameter,
            IList<CharacterId> targetIdList)
        {
            _ailmentGenerator = ailmentGenerator;
            _battleLoggerService = battleLoggerService;
            _skillCommon = skillCommon;
            _ailmentParameter = ailmentParameter;
            _targetIdList = targetIdList.ToImmutableList();
        }

        public override void Start()
        {
            var ailment = _ailmentGenerator.Generate(
                skillCommon: _skillCommon,
                ailmentParameter: _ailmentParameter,
                targetIdList: _targetIdList);
            _battleLoggerService.Log(ailment);
        }
    }
}