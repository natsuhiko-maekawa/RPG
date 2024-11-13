using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Id;
using BattleScene.InterfaceAdapter.State.Turn;

namespace BattleScene.InterfaceAdapter.StateMachine
{
    public class TurnStateMachine
    {
        private Context _context = null!;
        private readonly List<Memento> _mementoList = new();
        private readonly TurnStartState _turnStartState;

        public TurnStateMachine(
            TurnStartState turnStartState)
        {
            _turnStartState = turnStartState;
        }

        public void Start()
        {
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
            _mementoList.Add(_context.Save());
        }

        private void Undo()
        {
            if (_mementoList.Count == 0) return;

            var memento = _mementoList.Last();
            _mementoList.Remove(memento);
            _context.Restore(memento);
        }
    }
}