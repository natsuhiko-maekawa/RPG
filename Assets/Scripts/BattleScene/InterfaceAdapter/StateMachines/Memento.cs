using BattleScene.InterfaceAdapter.States.Turn;

namespace BattleScene.InterfaceAdapter.StateMachines
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