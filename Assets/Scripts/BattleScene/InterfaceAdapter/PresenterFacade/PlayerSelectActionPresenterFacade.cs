using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.PresenterFacade
{
    public class PlayerSelectActionPresenterFacade
    {
        private readonly InfoViewPresenter _infoView;
        private readonly TableViewPresenter _tableView;

        public PlayerSelectActionPresenterFacade(
            InfoViewPresenter infoView,
            TableViewPresenter tableView)
        {
            _infoView = infoView;
            _tableView = tableView;
        }

        public void Output()
        {
            _infoView.StartAnimation(MessageCode.VerticalSelect);
            _tableView.StartAnimation();
        }

        public void Stop()
        {
            _infoView.StopAnimation();
            _tableView.Stop();
        }
    }
}