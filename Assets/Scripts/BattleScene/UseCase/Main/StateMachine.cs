namespace BattleScene.UseCase.Main
{
    internal class StateMachine
    {
        private State _state;
        private readonly StateFactory _stateFactory;

        public StateMachine(
            StateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }

        public void Start()
        {
            _state = _stateFactory.Create(StateCode.Initialize);
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
            _state = _stateFactory.Create(stateCode);
            _state.Start();
            _state.Update();
        }
    }
}