using BattleScene.Domain.DomainService;
using VContainer;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    internal class TurnEndState : BaseState
    {
        private readonly AilmentDomainService _ailment;
        //TODO: 循環参照を避けるための暫定的な処置
        private readonly IObjectResolver _container;
        private readonly SlipDomainService _slip;

        public TurnEndState(
            AilmentDomainService ailment,
            IObjectResolver container,
            SlipDomainService slip)
        {
            _ailment = ailment;
            _container = container;
            _slip = slip;
        }

        public override void Start()
        {
            _ailment.AdvanceTurn();
            _slip.AdvanceTurn();
            Context.TransitionTo(_container.Resolve<OrderState>());
        }
    }
}