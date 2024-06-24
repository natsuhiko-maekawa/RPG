using BattleScene.UseCases.StateMachine.Service;

namespace BattleScene.UseCases.StateMachine
{
    internal class StateMachine
    {
        private State _state;
        private readonly StateCreatorService _stateCreator;

        public StateMachine(
            StateCreatorService stateCreator)
        {
            _stateCreator = stateCreator;
        }

        public void Start()
        {
            _state = _stateCreator.Create(StateCode.Initialize);
            _state.Start();
            _state.Update();
        }

        public void Update()
        {
            var stateCode = _state.Triggers();
            if (stateCode == StateCode.NoTrigger)
            {
                _state.Update();
                return;
            }
            
            _state.Stop();
            _state = _stateCreator.Create(stateCode);
            _state.Start();
            _state.Update();
        }
    }
}