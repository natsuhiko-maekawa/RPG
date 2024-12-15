using System;
using BattleScene.Presenters.PresenterFacades;
using BattleScene.Presenters.StateMachines;
using BattleScene.UseCases.UseCases;

namespace BattleScene.Presenters.States.Turn
{
    public class SkillState : BaseState
    {
        private readonly SkillUseCaseInTurnState _useCaseInTurnState;
        private readonly SkillPresenterFacade _facade;
        private readonly SkillStateMachine _skillStateMachine;
        private readonly CharacterDeadState _characterDeadState;
        private readonly AdvanceTurnState _advanceTurnState;

        public SkillState(
            SkillUseCaseInTurnState useCaseInTurnState,
            SkillPresenterFacade facade,
            SkillStateMachine skillStateMachine,
            CharacterDeadState characterDeadState,
            AdvanceTurnState advanceTurnState)
        {
            _useCaseInTurnState = useCaseInTurnState;
            _facade = facade;
            _skillStateMachine = skillStateMachine;
            _characterDeadState = characterDeadState;
            _advanceTurnState = advanceTurnState;
        }

        public override void Start()
        {
            var actor = Context.Actor ?? throw new InvalidOperationException(ExceptionMessage.ContextActorIdIsNull);
            var skill = Context.Skill ?? throw new InvalidOperationException(ExceptionMessage.ContextSkillIsNull);
            _useCaseInTurnState.ExecuteSkill(actor, skill, Context.BattleEventCode);
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