using System;
using System.Collections.Generic;
using BattleScene.Domain.Entity;
using BattleScene.InterfaceAdapter.StateMachine;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class TurnState : BaseState
    {
        private readonly TurnStateMachine _turnStateMachine;
        private readonly PlayerLoseState _playerLoseState;
        private readonly PlayerWinState _playerWinState;

        public TurnState(
            TurnStateMachine turnStateMachine,
            PlayerLoseState playerLoseState,
            PlayerWinState playerWinState)
        {
            _turnStateMachine = turnStateMachine;
            _playerLoseState = playerLoseState;
            _playerWinState = playerWinState;
        }

        public override void Start()
        {
            _turnStateMachine.Start();
        }

        public override void Select()
        {
            var isContinue = _turnStateMachine.TryOnSelect(out var nextStateCode);
            if (isContinue) return;

            if (nextStateCode == StateCode.Next)
            {
                Start();
            }
            else
            {
                Context.TransitionTo(GetNextState(nextStateCode));
            }
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

        private BaseState GetNextState(StateCode nextStateCode)
        {
            BaseState nextState = nextStateCode switch
            {
                StateCode.PlayerLoseState => _playerLoseState,
                StateCode.PlayerWinState => _playerWinState,
                _ => throw new ArgumentOutOfRangeException(nameof(nextStateCode), nextStateCode, null)
            };

            return nextState;
        }
    }
}