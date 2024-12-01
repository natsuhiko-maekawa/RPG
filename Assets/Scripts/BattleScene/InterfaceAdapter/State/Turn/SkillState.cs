using System;
using BattleScene.InterfaceAdapter.PresenterFacade;
using BattleScene.InterfaceAdapter.StateMachine;
using BattleScene.UseCases.UseCase;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    public class SkillState : BaseState
    {
        private readonly SkillUseCase _useCase;
        private readonly SkillElementStateMachine _skillElementStateMachine;
        private readonly SkillPresenterFacade _facade;
        private readonly AdvanceTurnState _advanceTurnState;

        public SkillState(
            SkillUseCase useCase,
            SkillElementStateMachine skillElementStateMachine,
            SkillPresenterFacade facade,
            AdvanceTurnState advanceTurnState)
        {
            _useCase = useCase;
            _skillElementStateMachine = skillElementStateMachine;
            _facade = facade;
            _advanceTurnState = advanceTurnState;
        }

        public override void Start()
        {
            if (Context.ActorId == null) throw new InvalidOperationException(ExceptionMessage.ContextActorIdIsNull);
            if (Context.Skill == null) throw new InvalidOperationException(ExceptionMessage.ContextSkillIsNull);
            _useCase.ExecuteSkill(Context.ActorId, Context.Skill);
            _facade.Output(Context);
        }

        public override void Select()
        {
            var value = _skillElementStateMachine.Select(Context);
            if (!value) Context.TransitionTo(_advanceTurnState);
        }
    }
}