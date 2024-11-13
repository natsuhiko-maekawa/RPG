using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.InterfaceAdapter.State.Turn;
using Utility;

namespace BattleScene.InterfaceAdapter.StateMachine
{
    public class TurnStateMachine
    {
        private Context _context = null!;
        private readonly Stack<Memento> _mementoStack = new();
        private readonly TurnStartState _turnStartState;

        public TurnStateMachine(
            TurnStartState turnStartState)
        {
            _turnStartState = turnStartState;
        }

        public void Start()
        {
            _mementoStack.Clear();
            _context = new Context(_turnStartState);
        }

        public bool OnSelect()
        {
            Backup();
            _context.Select();
            return _context.IsContinue;
        }

        public void OnSelect(int id)
        {
            Backup();
            _context.Select(id);
        }

        public void OnSelect(IReadOnlyList<CharacterId> targetIdList)
        {
            Backup();
            _context.Select(targetIdList);
        }

        public void OnCancel()
        {
            if (!_context.TryCancel()) return;
            Undo();
            _context.Start();
        }

        private void Backup()
        {
            // contextが保持しているステートがキャンセルできない場合、
            // 前回BackupしたMementoはUndoされる可能性がないため、Popする。
            // StateMachineを初期化してすぐの場合mementoStackは空なので、
            // PopメソッドではなくTryPopメソッドを用いる。
            if (!_context.HasCancelableState) _mementoStack.TryPop(out _);
            _mementoStack.Push(_context.Save());
            // 現状の状態遷移だとmementoStackのsizeが4を超えることはない。
            MyDebug.Assert(_mementoStack.Count < 4);
        }

        private void Undo()
        {
            if (!_mementoStack.TryPop(out var memento)) return;
            _context.Restore(memento);
        }
    }
}