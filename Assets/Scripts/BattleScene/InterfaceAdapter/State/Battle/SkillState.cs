using BattleScene.InterfaceAdapter.Facade;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    internal class SkillState : BaseState
    {
        private readonly SkillExecutorService _skillExecutor;
        private readonly PrimeSkillStateMachine _primeSkillStateMachine;
        private readonly SkillOutputFacade _skillOutput;
        private readonly TurnEndState _turnEndState;

        public SkillState(
            SkillExecutorService skillExecutor,
            PrimeSkillStateMachine primeSkillStateMachine,
            SkillOutputFacade skillOutput,
            TurnEndState turnEndState)
        {
            _skillExecutor = skillExecutor;
            _primeSkillStateMachine = primeSkillStateMachine;
            _skillOutput = skillOutput;
            _turnEndState = turnEndState;
        }

        public override async void Start()
        {
            _primeSkillStateMachine.Start(Context);
            _skillExecutor.Execute(Context.SkillCode);
            await _skillOutput.Output(Context);
        }

        public override void Select()
        {
            var value = _primeSkillStateMachine.Select();
            if (!value) Context.TransitionTo(_turnEndState);
        }
    }
}