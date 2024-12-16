using BattleScene.Presenters.PresenterFacades;

namespace BattleScene.Presenters.States.Battle
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