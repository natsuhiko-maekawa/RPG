﻿using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class AilmentState : AbstractSkillState
    {
        private readonly IReadOnlyList<AilmentValueObject> _ailmentList;
        private readonly AilmentRegistererService _ailmentRegisterer;
        private readonly BattleLoggerService _battleLoggerService;
        private readonly AilmentParameterValueObject _ailmentParameter;
        private readonly AilmentMessageState _ailmentMessageState;
        private readonly AilmentFailureState _ailmentFailureState;

        public AilmentState(
            IReadOnlyList<AilmentValueObject> ailmentList, 
            AilmentRegistererService ailmentRegisterer,
            BattleLoggerService battleLoggerService,
            AilmentMessageState ailmentMessageState,
            AilmentFailureState ailmentFailureState)
        {
            _ailmentList = ailmentList;
            _ailmentRegisterer = ailmentRegisterer;
            _battleLoggerService = battleLoggerService;
            _ailmentMessageState = ailmentMessageState;
            _ailmentFailureState = ailmentFailureState;
        }

        public override void Start()
        {
            _ailmentRegisterer.Register(_ailmentList);
            _battleLoggerService.Log(_ailmentList);
            var failure = _ailmentList
                .All(x => x.ActualTargetIdList.Count == 0);
            AbstractSkillState nextState = failure
                ? _ailmentFailureState 
                : _ailmentMessageState;
            SkillContext.TransitionTo(nextState);
        }
    }
}