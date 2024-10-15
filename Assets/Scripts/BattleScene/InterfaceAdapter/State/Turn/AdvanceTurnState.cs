using BattleScene.Domain.DomainService;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    internal class AdvanceTurnState : BaseState
    {
        private readonly AilmentDomainService _ailment;
        private readonly BuffTurnService _buffTurn;
        private readonly SlipDomainService _slip;
        private readonly TurnStopState _turnStopState;

        public AdvanceTurnState(
            AilmentDomainService ailment,
            BuffTurnService buffTurn,
            SlipDomainService slip,
            TurnStopState turnStopState)
        {
            _ailment = ailment;
            _buffTurn = buffTurn;
            _slip = slip;
            _turnStopState = turnStopState;
        }

        public override void Start()
        {
            _ailment.AdvanceTurn();
            _buffTurn.Advance();
            _slip.AdvanceTurn();
            Context.TransitionTo(_turnStopState);
        }
    }
}