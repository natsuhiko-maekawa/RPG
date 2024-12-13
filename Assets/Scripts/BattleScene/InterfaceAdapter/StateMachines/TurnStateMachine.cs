using System.Collections.Generic;
using BattleScene.Domain.Entities;
using BattleScene.InterfaceAdapter.States;
using BattleScene.InterfaceAdapter.States.Turn;
using Utility;

namespace BattleScene.InterfaceAdapter.StateMachines
{
    public class TurnStateMachine
    {
        private Context _context = null!;
        // NOTE: 現段階で保持する画面の最大数は4つだが、変更があった場合はキャパシティを修正すること。
        private readonly Stack<Memento> _mementoStack = new(4);
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
        public bool TryOnSelect(out StateCode nextStateCode)
        {
            Backup();
            _context.Select();
            nextStateCode = _context.NextStateCode;
            return _context.IsContinue;
        }

        public void OnSelect(int id)
        {
            Backup();
            _context.Select(id);
        }

        public void OnSelect(IReadOnlyList<CharacterEntity> targetList)
        {
            Backup();
            _context.Select(targetList);
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
            if (!_context.IsCancelable) _mementoStack.TryPop(out _);
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