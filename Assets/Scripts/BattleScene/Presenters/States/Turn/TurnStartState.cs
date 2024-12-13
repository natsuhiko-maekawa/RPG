using BattleScene.Presenters.Presenters;
using BattleScene.Presenters.Services;
using BattleScene.UseCases.UseCases;

namespace BattleScene.Presenters.States.Turn
{
    public class TurnStartState : BaseState
    {
        private readonly OrderUseCase _order;
        private readonly ActorService _actor;
        private readonly OrderViewPresenter _orderView;
        private readonly PlayerSelectActionState _playerSelectActionState;
        private readonly EnemySelectActionState _enemySelectActionState;
        private readonly CantActionState _cantActionState;
        private readonly SlipDamageState _slipDamageState;
        private readonly ResetAilmentState _resetAilmentState;

        public TurnStartState(
            OrderUseCase order,
            ActorService actor,
            OrderViewPresenter orderView,
            PlayerSelectActionState playerSelectActionState,
            EnemySelectActionState enemySelectActionState,
            CantActionState cantActionState,
            SlipDamageState slipDamageState,
            ResetAilmentState resetAilmentState)
        {
            _order = order;
            _actor = actor;
            _orderView = orderView;
            _playerSelectActionState = playerSelectActionState;
            _enemySelectActionState = enemySelectActionState;
            _cantActionState = cantActionState;
            _slipDamageState = slipDamageState;
            _resetAilmentState = resetAilmentState;
        }

        public override void Start()
        {
            _order.Register();
            (Context.Actor, Context.AilmentCode, Context.SlipCode) = _order.First();
            _orderView.StartAnimation();
            var nextState = GetNextState();
            Context.TransitionTo(nextState);
        }

        private BaseState GetNextState()
        {
            if (_actor.IsResetAilment(Context.AilmentCode)) return _resetAilmentState;
            if (_actor.IsSlipDamage(Context.SlipCode)) return _slipDamageState;
            if (_actor.CantAction(Context.Actor, out var skill))
            {
                Context.Skill = skill;
                return _cantActionState;
            }

            return _actor.IsPlayer(Context.Actor)
                ? _playerSelectActionState
                : _enemySelectActionState;
        }
    }
}