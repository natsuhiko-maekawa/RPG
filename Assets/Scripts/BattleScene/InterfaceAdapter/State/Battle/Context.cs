using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Id;
using Utility;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class Context
    {
        private BaseState _state;

        public Context(BaseState state)
        {
            TransitionTo(state);
        }

        public void TransitionTo(BaseState state)
        {
            MyDebug.Log(state.GetType().Name);
            _state = state;
            _state.SetContext(this);
            _state.Start();
        }

        public void Select() => _state.Select();

        public void Select(int id) => _state.Select(id);

        public void Select(IReadOnlyList<CharacterId> targetIdList) => _state.Select(targetIdList);
    }
}