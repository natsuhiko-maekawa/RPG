using System;
using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.StateMachine;
using Utility;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    public class Context
    {
        private BaseState _state = null!;

        public CharacterId? ActorId { get; set; }
        public AilmentCode AilmentCode { get; set; }
        public SlipCode SlipCode { get; set; }
        public SkillValueObject? Skill { get; set; }
        public IReadOnlyList<CharacterId> TargetIdList { get; set; } = Array.Empty<CharacterId>();

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

        public void Start() => _state.Start();

        public void Select() => _state.Select();

        public void Select(int id) => _state.Select(id);

        public void Select(IReadOnlyList<CharacterId> targetIdList) => _state.Select(targetIdList);

        public Memento Save()
        {
            return new Memento(_state);
        }

        public void Restore(Memento memento)
        {
            _state = memento.GetState();
        }

        public bool HasStopState => _state is TurnStopState;
        public bool HasCancelableState => _state is ICancelable;

        public bool TryCancel()
        {
            if (_state is not ICancelable cancelable) return false;
            cancelable.OnCancel();
            return true;
        }
    }
}