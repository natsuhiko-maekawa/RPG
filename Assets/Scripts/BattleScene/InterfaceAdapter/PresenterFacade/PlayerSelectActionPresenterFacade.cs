using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.PresenterFacade
{
    public class PlayerSelectActionPresenterFacade
    {
        private readonly InfoViewPresenter _infoView;
        private readonly GridViewPresenter _gridView;

        public PlayerSelectActionPresenterFacade(
            InfoViewPresenter infoView,
            GridViewPresenter gridView)
        {
            _infoView = infoView;
            _gridView = gridView;
        }

        public void Output()
        {
            _infoView.StartAnimation(MessageCode.VerticalSelect);
            _gridView.StartAnimation();
        }

        public void Stop()
        {
            _infoView.StopAnimation();
            _gridView.Stop();
        }
    }
}