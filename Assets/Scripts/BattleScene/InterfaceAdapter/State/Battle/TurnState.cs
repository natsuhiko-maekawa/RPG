using System.Collections.Generic;
using BattleScene.Domain.Entity;
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
            var isStop = _turnStateMachine.OnSelect();
            if (isStop) Start();
        }

        public override void Select(int id)
        {
            _turnStateMachine.OnSelect(id);
        }

        public override void Select(IReadOnlyList<CharacterEntity> targetList)
        {
            _turnStateMachine.OnSelect(targetList);
        }

        public override void Cancel()
        {
            _turnStateMachine.OnCancel();
        }
    }
}