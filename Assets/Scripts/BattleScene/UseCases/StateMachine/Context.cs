namespace BattleScene.UseCases.StateMachine
{
    public class Context
    {
        private AbstractState _state;

        public Context(AbstractState state)
        {
            TransitionTo(state);
        }

        public void TransitionTo(AbstractState state)
        {
            _state = state;
            _state.SetContext(this);
            _state.Start();
        }

        public void Select() => _state.Select();
    }
}