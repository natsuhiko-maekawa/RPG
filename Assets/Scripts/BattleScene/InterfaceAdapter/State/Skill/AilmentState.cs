﻿using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class AilmentState : AbstractSkillState
    {
        private readonly AilmentGeneratorService _ailmentGenerator;
        private readonly BattleLogDomainService _battleLog;
        private readonly BattleLoggerService _battleLoggerService;
        private readonly SkillCommonValueObject _skillCommon;
        private readonly AilmentParameterValueObject _ailmentParameter;
        private readonly ImmutableList<CharacterId> _targetIdList;
        private readonly AilmentMessageState _ailmentMessageState;
        private readonly AilmentFailureState _ailmentFailureState;

        public AilmentState(
            AilmentGeneratorService ailmentGenerator, 
            BattleLoggerService battleLoggerService,
            BattleLogDomainService battleLog,
            SkillCommonValueObject skillCommon,
            AilmentParameterValueObject ailmentParameter,
            IList<CharacterId> targetIdList,
            AilmentMessageState ailmentMessageState,
            AilmentFailureState ailmentFailureState)
        {
            _ailmentGenerator = ailmentGenerator;
            _battleLoggerService = battleLoggerService;
            _battleLog = battleLog;
            _skillCommon = skillCommon;
            _ailmentParameter = ailmentParameter;
            _targetIdList = targetIdList.ToImmutableList();
            _ailmentMessageState = ailmentMessageState;
            _ailmentFailureState = ailmentFailureState;
        }

        public override void Start()
        {
            var ailment = _ailmentGenerator.Generate(
                skillCommon: _skillCommon,
                ailmentParameter: _ailmentParameter,
                targetIdList: _targetIdList);
            _battleLoggerService.Log(ailment);
            AbstractSkillState nextState = _battleLog.GetLast().TargetIdList.IsEmpty
                ? _ailmentFailureState 
                : _ailmentMessageState;
            SkillContext.TransitionTo(nextState);
        }
    }
}