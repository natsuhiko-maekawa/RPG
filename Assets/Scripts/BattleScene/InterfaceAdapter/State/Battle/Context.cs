using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.State.Battle
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

        public void Select(int id)
        {
            switch (_state)
            {
                case PlayerSelectActionState:
                    _state.Select((ActionCode)id);
                    break;
                case PlayerSelectSkillState:
                    _state.Select((SkillCode)id);
                    break;
            }
        }
        
        public void Select(ImmutableList<CharacterId> targetIdList) => _state.Select(targetIdList);
    }
}