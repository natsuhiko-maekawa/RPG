using BattleScene.Domain.DomainService;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    internal class TurnStopState : BaseState
    {
        private readonly AilmentDomainService _ailment;
        private readonly SlipDomainService _slip;

        public TurnStopState(
            AilmentDomainService ailment,
            SlipDomainService slip)
        {
            _ailment = ailment;
            _slip = slip;
        }

        public override void Start()
        {
            _ailment.AdvanceTurn();
            _slip.AdvanceTurn();
        }
    }
}