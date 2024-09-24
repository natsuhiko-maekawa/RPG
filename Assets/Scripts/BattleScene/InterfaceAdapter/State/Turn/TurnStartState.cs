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

        public TurnStartState(
            ActionTimeService actionTime,
            OrderService order,
            ActorService actor,
            OrderViewPresenter orderView,
            PlayerSelectActionState playerSelectActionState,
            EnemySelectSkillState enemySelectSkillState)
        {
            _actionTime = actionTime;
            _order = order;
            _actor = actor;
            _orderView = orderView;
            _playerSelectActionState = playerSelectActionState;
            _enemySelectSkillState = enemySelectSkillState;
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
            if (_actor.IsSlipDamage) throw new NotImplementedException();
            if (_actor.CantAction) throw new NotImplementedException();
            return _actor.IsPlayer 
                ? _playerSelectActionState 
                : _enemySelectSkillState;
        }
    }
}