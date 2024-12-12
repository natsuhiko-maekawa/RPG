using Utility;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class PlayerLoseState : BaseState
    {
        public override void Start()
        {
            MyDebug.Log("you lose...");
        }
    }
}