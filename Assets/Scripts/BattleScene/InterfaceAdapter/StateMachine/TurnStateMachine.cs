using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.InterfaceAdapter.State.Turn;
using Utility;

namespace BattleScene.InterfaceAdapter.StateMachine
{
    public class TurnStateMachine
    {
        private Context _context = null!;
        // NOTE: _mementoStackのキャパシティは保持する画面数(4)にする。
        // 現在は不具合があるため、5にしている。
        private readonly Stack<Memento> _mementoStack = new(10);
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

        /// <summary>
        /// 引数のない「決定」のアクションがあったときに、次の状態に遷移する。<br/>
        /// ステートマシンが終了した場合(これ以上遷移先のない状態に遷移した場合)、trueを返す。
        /// </summary>
        /// <returns>ステートマシンが終了した場合、true。それ以外の場合、false。</returns>
        public bool OnSelect()
        {
            Backup();
            _context.Select();
            return _context.HasStopState;
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
            if (_mementoStack.Count > 4) MyDebug.LogAssertion(ExceptionMessage.MementoStackSizeIsOver);
        }

        private void Undo()
        {
            if (!_mementoStack.TryPop(out var memento)) return;
            _context.Restore(memento);
        }
    }
}