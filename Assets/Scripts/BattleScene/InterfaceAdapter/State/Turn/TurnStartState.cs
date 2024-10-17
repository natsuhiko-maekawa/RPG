using BattleScene.InterfaceAdapter.Presenter;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.UseCases.UseCase;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    public class TurnStartState : BaseState
    {
        private readonly OrderUseCase _order;
        private readonly ActorService _actor;
        private readonly OrderViewPresenter _orderView;
        private readonly PlayerSelectActionState _playerSelectActionState;
        private readonly EnemySelectSkillState _enemySelectSkillState;
        private readonly CantActionState _cantActionState;
        private readonly SlipState _slipState;
        private readonly ResetAilmentState _resetAilmentState;

        public TurnStartState(
            OrderUseCase order,
            ActorService actor,
            OrderViewPresenter orderView,
            PlayerSelectActionState playerSelectActionState,
            EnemySelectSkillState enemySelectSkillState,
            CantActionState cantActionState,
            SlipState slipState,
            ResetAilmentState resetAilmentState)
        {
            _order = order;
            _actor = actor;
            _orderView = orderView;
            _playerSelectActionState = playerSelectActionState;
            _enemySelectSkillState = enemySelectSkillState;
            _cantActionState = cantActionState;
            _slipState = slipState;
            _resetAilmentState = resetAilmentState;
        }

        public override void Start()
        {
            _order.Register();
            (Context.ActorId, Context.AilmentCode, Context.SlipCode) = _order.First();
            _orderView.StartAnimationAsync();
            var nextState = GetNextState();
            Context.TransitionTo(nextState);
        }

        private BaseState GetNextState()
        {
            if (_actor.IsResetAilment) return _resetAilmentState;
            if (_actor.IsSlipDamage) return _slipState;
            if (_actor.CantAction) return _cantActionState;
            return _actor.IsPlayer
                ? _playerSelectActionState
                : _enemySelectSkillState;
        }
    }
}