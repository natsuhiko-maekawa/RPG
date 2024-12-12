using BattleScene.InterfaceAdapter.PresenterFacade;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class PlayerWinState : BaseState
    {
        private readonly PlayerWinPresenterFacade _facade;

        public PlayerWinState(
            PlayerWinPresenterFacade facade)
        {
            _facade = facade;
        }

        public override void Start()
        {
            _facade.Output();
        }
    }
}