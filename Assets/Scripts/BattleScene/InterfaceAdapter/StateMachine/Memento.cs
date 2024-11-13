using BattleScene.InterfaceAdapter.State.Turn;

namespace BattleScene.InterfaceAdapter.StateMachine
{
    public class Memento
    {
        private readonly BaseState _state;

        public Memento(BaseState state)
        {
            _state = state;
        }

        public BaseState GetState()
        {
            return _state;
        }
    }
}