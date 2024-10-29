using System;
using BattleScene.InterfaceAdapter.Facade;
using BattleScene.InterfaceAdapter.StateMachine;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    public class SkillState : BaseState
    {
        private readonly SkillExecutorService _skillExecutor;
        private readonly PrimeSkillStateMachine _primeSkillStateMachine;
        private readonly SkillOutputFacade _skillOutput;
        private readonly AdvanceTurnState _advanceTurnState;

        public SkillState(
            SkillExecutorService skillExecutor,
            PrimeSkillStateMachine primeSkillStateMachine,
            SkillOutputFacade skillOutput,
            AdvanceTurnState advanceTurnState)
        {
            _skillExecutor = skillExecutor;
            _primeSkillStateMachine = primeSkillStateMachine;
            _skillOutput = skillOutput;
            _advanceTurnState = advanceTurnState;
        }

        public override async void Start()
        {
            if (Context.Skill == null) throw new InvalidOperationException(ExceptionMessage.ContextSkillIsNull);
            _skillExecutor.Execute(Context.Skill);
            await _skillOutput.Output(Context);
        }

        public override void Select()
        {
            var value = _primeSkillStateMachine.Select(Context);
            if (!value) Context.TransitionTo(_advanceTurnState);
        }
    }
}