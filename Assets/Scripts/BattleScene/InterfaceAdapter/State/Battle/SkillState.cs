using BattleScene.InterfaceAdapter.Facade;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.UseCases.Service;
using VContainer;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class SkillState : BaseState
    {
        private readonly IObjectResolver _container;
        private readonly SkillExecutorService _skillExecutor;
        private readonly PrimeSkillContextService _primeSkillContext;
        private readonly SkillOutputFacade _skillOutput;

        public SkillState(
            IObjectResolver container,
            SkillExecutorService skillExecutor,
            PrimeSkillContextService primeSkillContext,
            SkillOutputFacade skillOutput)
        {
            _container = container;
            _skillExecutor = skillExecutor;
            _primeSkillContext = primeSkillContext;
            _skillOutput = skillOutput;
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
            
            if (!value)
            {
                Context.TransitionTo(_container.Resolve<TurnEndState>());
            }
        }
    }
}