using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    public class TurnStopState : BaseState
    {
        private readonly TurnService _turn;

        public TurnStopState(
            TurnService turn)
        {
            _turn = turn;
        }

        public override void Start()
        {
            _turn.Increment();
        }
    }
}