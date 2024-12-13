using BattleScene.UseCases.Service;
using Utility;

namespace BattleScene.InterfaceAdapter.States.Turn
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
            MyDebug.Assert(Context.NextStateCode is not StateCode.None and not StateCode.Next);
            _turn.Increment();
        }
    }
}