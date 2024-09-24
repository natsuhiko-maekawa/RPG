using BattleScene.InterfaceAdapter.Facade;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    internal class SkillState : BaseState
    {
        private readonly SkillExecutorService _skillExecutor;
        private readonly PrimeSkillStateMachine _primeSkillStateMachine;
        private readonly SkillOutputFacade _skillOutput;
        private readonly TurnStopState _turnStopState;

        public SkillState(
            SkillExecutorService skillExecutor,
            PrimeSkillStateMachine primeSkillStateMachine,
            SkillOutputFacade skillOutput,
            TurnStopState turnStopState)
        {
            _skillExecutor = skillExecutor;
            _primeSkillStateMachine = primeSkillStateMachine;
            _skillOutput = skillOutput;
            _turnStopState = turnStopState;
        }

        public override async void Start()
        {
            _skillExecutor.Execute(Context.SkillCode);
            await _skillOutput.Output(Context);
        }

        public override void Select()
        {
            var value = _primeSkillStateMachine.Select(Context);
            if (!value) Context.TransitionTo(_turnStopState);
        }
    }
}