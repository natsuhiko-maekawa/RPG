using System;
using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.StateMachine;
using Utility;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    public class Context
    {
        private BaseState _state = null!;

        public CharacterEntity? Actor { get; set; }
        public AilmentCode AilmentCode { get; set; }
        public SlipCode SlipCode { get; set; }
        public SkillValueObject? Skill { get; set; }
        public IReadOnlyList<CharacterEntity> TargetList { get; set; } = Array.Empty<CharacterEntity>();

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

        public void Select(IReadOnlyList<CharacterEntity> targetList) => _state.Select(targetList);

        public Memento Save()
        {
            return new Memento(_state);
        }

        public void Restore(Memento memento)
        {
            _state = memento.GetState();
        }

        public bool IsContinue => _state is not TurnStopState;
        public bool IsCancelable => _state is ICancelable;
        public StateCode NextStateCode { get; set; } = StateCode.Current;

        public bool TryCancel()
        {
            if (_state is not ICancelable cancelable) return false;
            cancelable.OnCancel();
            return true;
        }
    }
}