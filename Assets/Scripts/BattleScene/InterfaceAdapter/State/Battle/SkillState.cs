using BattleScene.InterfaceAdapter.Facade;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    internal class SkillState : BaseState
    {
        private readonly SkillExecutorService _skillExecutor;
        private readonly PrimeSkillContextService _primeSkillContext;
        private readonly SkillOutputFacade _skillOutput;
        private readonly TurnEndState _turnEndState;

        public SkillState(
            SkillExecutorService skillExecutor,
            PrimeSkillContextService primeSkillContext,
            SkillOutputFacade skillOutput,
            TurnEndState turnEndState)
        {
            _skillExecutor = skillExecutor;
            _primeSkillContext = primeSkillContext;
            _skillOutput = skillOutput;
            _turnEndState = turnEndState;
        }

        public override async void Start()
        {
            _primeSkillContext.Start(Context);
            _skillExecutor.Execute(Context.SkillCode);
            await _skillOutput.Output(Context);
        }

        public override void Select()
        {
            var value = _primeSkillContext.Select();
            if (!value) Context.TransitionTo(_turnEndState);
        }
    }
}