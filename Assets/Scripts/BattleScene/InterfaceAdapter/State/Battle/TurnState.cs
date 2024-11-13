using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.InterfaceAdapter.StateMachine;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class TurnState : BaseState
    {
        private readonly TurnStateMachine _turnStateMachine;

        public TurnState(
            TurnStateMachine turnStateMachine)
        {
            _turnStateMachine = turnStateMachine;
        }

        public override void Start()
        {
            _turnStateMachine.Start();
        }

        public override void Select()
        {
            var isContinue = _turnStateMachine.OnSelect();
            if (!isContinue) Start();
        }

        public override void Select(int id)
        {
            _turnStateMachine.OnSelect(id);
        }

        public override void Select(IReadOnlyList<CharacterId> targetIdList)
        {
            _turnStateMachine.OnSelect(targetIdList);
        }

        public override void Cancel()
        {
            _turnStateMachine.OnCancel();
        }
    }
}