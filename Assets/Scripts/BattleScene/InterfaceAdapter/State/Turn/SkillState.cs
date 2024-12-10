﻿using System;
using BattleScene.InterfaceAdapter.PresenterFacade;
using BattleScene.InterfaceAdapter.StateMachine;
using BattleScene.UseCases.UseCase;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    public class SkillState : BaseState
    {
        private readonly SkillUseCase _useCase;
        private readonly SkillPresenterFacade _facade;
        private readonly SkillElementStateMachine _skillElementStateMachine;
        private readonly CharacterDeadState _characterDeadState;
        private readonly AdvanceTurnState _advanceTurnState;

        public SkillState(
            SkillUseCase useCase,
            SkillPresenterFacade facade,
            SkillElementStateMachine skillElementStateMachine,
            CharacterDeadState characterDeadState,
            AdvanceTurnState advanceTurnState)
        {
            _useCase = useCase;
            _facade = facade;
            _skillElementStateMachine = skillElementStateMachine;
            _characterDeadState = characterDeadState;
            _advanceTurnState = advanceTurnState;
        }

        public override void Start()
        {
            var actor = Context.Actor ?? throw new InvalidOperationException(ExceptionMessage.ContextActorIdIsNull);
            var skill = Context.Skill ?? throw new InvalidOperationException(ExceptionMessage.ContextSkillIsNull);
            _useCase.ExecuteSkill(actor, skill);
            _facade.Output(Context);
        }

        public override void Select()
        {
            var value = _skillElementStateMachine.TrySelect(Context, out var nextStateCode);
            if (!value) Context.TransitionTo(GetNextState(nextStateCode));
        }

        private BaseState GetNextState(StateCode nextStateCode)
        {
            BaseState nextState = nextStateCode switch
            {
                StateCode.AdvanceTurnState => _advanceTurnState,
                StateCode.CharacterDeadState => _characterDeadState,
                _ => throw new ArgumentOutOfRangeException(nameof(nextStateCode), nextStateCode, null)
            };

            return nextState;
        }
    }
}