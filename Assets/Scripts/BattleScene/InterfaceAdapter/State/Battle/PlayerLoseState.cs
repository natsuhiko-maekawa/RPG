using BattleScene.InterfaceAdapter.PresenterFacade;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class PlayerLoseState : BaseState
    {
        private readonly PlayerLosePresenterFacade _facade;

        public PlayerLoseState(
            PlayerLosePresenterFacade facade)
        {
            _facade = facade;
        }

        public override void Start()
        {
            _facade.Output();
        }
    }
}