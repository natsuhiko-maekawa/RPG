using System;
using BattleScene.InterfaceAdapter.Facade;
using BattleScene.InterfaceAdapter.StateMachine;
using BattleScene.UseCases.UseCase;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    public class SkillState : BaseState
    {
        private readonly SkillUseCase _useCase;
        private readonly PrimeSkillStateMachine _primeSkillStateMachine;
        private readonly SkillOutputFacade _skillOutput;
        private readonly AdvanceTurnState _advanceTurnState;

        public SkillState(
            SkillUseCase useCase,
            PrimeSkillStateMachine primeSkillStateMachine,
            SkillOutputFacade skillOutput,
            AdvanceTurnState advanceTurnState)
        {
            _useCase = useCase;
            _primeSkillStateMachine = primeSkillStateMachine;
            _skillOutput = skillOutput;
            _advanceTurnState = advanceTurnState;
        }

        public override async void Start()
        {
            if (Context.ActorId == null) throw new InvalidOperationException(ExceptionMessage.ContextActorIdIsNull);
            if (Context.Skill == null) throw new InvalidOperationException(ExceptionMessage.ContextSkillIsNull);
            _useCase.ExecuteSkill(Context.ActorId, Context.Skill);
            await _skillOutput.Output(Context);
        }

        public override void Select()
        {
            var value = _primeSkillStateMachine.Select(Context);
            if (!value) Context.TransitionTo(_advanceTurnState);
        }
    }
}