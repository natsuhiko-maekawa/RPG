using BattleScene.Domain.Codes;
using BattleScene.Presenters.Presenters;

namespace BattleScene.Presenters.PresenterFacades
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