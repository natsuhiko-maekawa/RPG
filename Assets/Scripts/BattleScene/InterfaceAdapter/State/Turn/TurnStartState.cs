using System;
using BattleScene.InterfaceAdapter.Presenter;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    internal class TurnStartState : BaseState
    {
        private readonly ActionTimeService _actionTime;
        private readonly OrderService _order;
        private readonly ActorService _actor;
        private readonly OrderViewPresenter _orderView;
        private readonly PlayerSelectActionState _playerSelectActionState;
        private readonly EnemySelectSkillState _enemySelectSkillState;
        private readonly CantActionState _cantActionState;
        private readonly SlipState _slipState;

        public TurnStartState(
            ActionTimeService actionTime,
            OrderService order,
            ActorService actor,
            OrderViewPresenter orderView,
            PlayerSelectActionState playerSelectActionState,
            EnemySelectSkillState enemySelectSkillState,
            CantActionState cantActionState,
            SlipState slipState)
        {
            _actionTime = actionTime;
            _order = order;
            _actor = actor;
            _orderView = orderView;
            _playerSelectActionState = playerSelectActionState;
            _enemySelectSkillState = enemySelectSkillState;
            _cantActionState = cantActionState;
            _slipState = slipState;
        }

        public override void Start()
        {
            _order.Update();
            _actionTime.Update();
            _orderView.StartAnimationAsync();
            var nextState = GetNextState();
            Context.TransitionTo(nextState);
        }

        private BaseState GetNextState()
        {
            if (_actor.IsResetAilment) throw new NotImplementedException();
            if (_actor.IsSlipDamage) return _slipState;
            if (_actor.CantAction) return _cantActionState;
            return _actor.IsPlayer 
                ? _playerSelectActionState 
                : _enemySelectSkillState;
        }
    }
}