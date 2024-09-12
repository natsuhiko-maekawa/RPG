using BattleScene.Domain.DomainService;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    internal class TurnEndState : AbstractState
    {
        private readonly AilmentDomainService _ailment;
        private readonly OrderState _orderState;

        public TurnEndState(
            AilmentDomainService ailment,
            OrderState orderState)
        {
            _ailment = ailment;
            _orderState = orderState;
        }

        public override void Select()
        {
            _ailment.AdvanceTurn();
            Context.TransitionTo(_orderState);
        }
    }
}