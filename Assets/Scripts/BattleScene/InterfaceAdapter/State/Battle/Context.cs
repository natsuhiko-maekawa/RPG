using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class Context
    {
        private BaseState _state;
        
        public SkillCode SkillCode { get; set; }
        public IReadOnlyList<CharacterId> TargetIdList { get; set; }

        public Context(BaseState state)
        {
            TransitionTo(state);
        }

        public void TransitionTo(BaseState state)
        {
            Debug.Log(state.GetType().Name);
            _state = state;
            _state.SetContext(this);
            _state.Start();
        }

        public void Select() => _state.Select();

        public void Select(int id) => _state.Select(id);
        
        public void Select(ImmutableList<CharacterId> targetIdList) => _state.Select(targetIdList);
    }
}