using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.InterfaceAdapter.State.Turn;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class TurnState : BaseState
    {
        private readonly TurnStartState _turnStartState;
        private Turn.Context _context = null!;

        public TurnState(
            TurnStartState turnStartState)
        {
            _turnStartState = turnStartState;
        }

        public override void Start()
        {
            _context = new Turn.Context(_turnStartState);
        }

        public override void Select()
        {
            _context.Select();
            Transition();
        }

        public override void Select(int id)
        {
            _context.Select(id);
            Transition();
        }

        public override void Select(IReadOnlyList<CharacterId> targetIdList)
        {
            _context.Select(targetIdList);
            Transition();
        }

        private void Transition()
        {
            if (!_context.IsContinue) Start();
        }
    }
}