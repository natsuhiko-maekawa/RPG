namespace BattleScene.InterfaceAdapter.State.Battle
{
    internal class TurnEndState : AbstractState
    {
        private readonly OrderState _orderState;

        public TurnEndState(
            OrderState orderState)
        {
            _orderState = orderState;
        }

        public override void Start()
        {
            Context.TransitionTo(_orderState);
        }
    }
}