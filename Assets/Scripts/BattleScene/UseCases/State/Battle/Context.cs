using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.UseCases.State.Battle
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
            Debug.Log(state.GetType().Name);
            _state = state;
            _state.SetContext(this);
            _state.Start();
        }

        public void Select() => _state.Select();
        public void Select(ActionCode actionCode) => _state.Select(actionCode);
    }
}