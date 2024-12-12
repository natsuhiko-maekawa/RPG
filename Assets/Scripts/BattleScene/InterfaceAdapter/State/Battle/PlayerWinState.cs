using Utility;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class PlayerWinState : BaseState
    {
        public override void Start()
        {
            MyDebug.Log("you win!");
        }
    }
}