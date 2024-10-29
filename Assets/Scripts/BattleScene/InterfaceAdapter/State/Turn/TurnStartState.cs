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
            (Context.ActorId, Context.AilmentCode, Context.SlipCode) = _order.First();
            _orderView.StartAnimationAsync();
            var nextState = GetNextState();
            Context.TransitionTo(nextState);
        }

        private BaseState GetNextState()
        {
            if (_actor.IsResetAilment(Context.AilmentCode)) return _resetAilmentState;
            if (_actor.IsSlipDamage(Context.SlipCode)) return _slipDamageState;
            if (_actor.CantActionBy(Context.ActorId, out var skill))
            {
                Context.Skill = skill;
                Context.SkillCode = Context.Skill.SkillCommon.SkillCode;
                return _cantActionState;
            }

            return _actor.IsPlayer(Context.ActorId)
                ? _playerSelectActionState
                : _enemySelectActionState;
        }
    }
}