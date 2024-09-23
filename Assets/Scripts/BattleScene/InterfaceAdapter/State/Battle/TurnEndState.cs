using BattleScene.Domain.DomainService;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    internal class TurnEndState : BaseState
    {
        private readonly AilmentDomainService _ailment;
        private readonly OrderState _orderState;
        private readonly SlipDomainService _slip;

        public TurnEndState(
            AilmentDomainService ailment,
            OrderState orderState,
            SlipDomainService slip)
        {
            _ailment = ailment;
            _orderState = orderState;
            _slip = slip;
        }

        public override void Start()
        {
            _ailment.AdvanceTurn();
            _slip.AdvanceTurn();
            Context.TransitionTo(_orderState);
        }
    }
}