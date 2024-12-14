using System;
using BattleScene.Domain.Codes;
using BattleScene.Presenters.PresenterFacades;
using BattleScene.Presenters.StateMachines;
using BattleScene.UseCases.UseCases;

namespace BattleScene.Presenters.States.Turn
{
    public class SkillState : BaseState
    {
        private readonly SkillUseCase _useCase;
        private readonly SkillPresenterFacade _facade;
        private readonly SkillStateMachine _skillStateMachine;
        private readonly CharacterDeadState _characterDeadState;
        private readonly AdvanceTurnState _advanceTurnState;

        public SkillState(
            SkillUseCase useCase,
            SkillPresenterFacade facade,
            SkillStateMachine skillStateMachine,
            CharacterDeadState characterDeadState,
            AdvanceTurnState advanceTurnState)
        {
            _useCase = useCase;
            _facade = facade;
            _skillStateMachine = skillStateMachine;
            _characterDeadState = characterDeadState;
            _advanceTurnState = advanceTurnState;
        }

        public override void Start()
        {
            var actor = Context.Actor ?? throw new InvalidOperationException(ExceptionMessage.ContextActorIdIsNull);
            var skill = Context.Skill ?? throw new InvalidOperationException(ExceptionMessage.ContextSkillIsNull);
            _useCase.ExecuteSkill(actor, skill, Context.BattleEventCode);
            _facade.Output(Context);
        }

        public override void Select()
        {
            var isContinue = _skillStateMachine.TryOnSelect(Context, out var nextStateCode);
            if (!isContinue) Context.TransitionTo(GetNextState(nextStateCode));
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