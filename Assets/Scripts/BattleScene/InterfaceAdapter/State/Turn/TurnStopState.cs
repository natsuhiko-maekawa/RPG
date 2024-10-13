using BattleScene.Domain.DomainService;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    internal class TurnStopState : BaseState
    {
        private readonly AilmentDomainService _ailment;
        private readonly BuffTurnService _buffTurn;
        private readonly SlipDomainService _slip;

        public TurnStopState(
            AilmentDomainService ailment,
            BuffTurnService buffTurn,
            SlipDomainService slip)
        {
            _ailment = ailment;
            _buffTurn = buffTurn;
            _slip = slip;
        }

        public override void Start()
        {
            _ailment.AdvanceTurn();
            _buffTurn.Advance();
            _slip.AdvanceTurn();
        }
    }
}