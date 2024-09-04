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

        public override void Select()
        {
            Context.TransitionTo(_orderState);
        }
    }
}