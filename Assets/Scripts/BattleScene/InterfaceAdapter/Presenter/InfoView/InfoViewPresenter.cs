using BattleScene.InterfaceAdapter.IView;
using BattleScene.UseCases.View.InfoView.OutputBoundary;

namespace BattleScene.InterfaceAdapter.Presenter.InfoView
{
    public class InfoViewPresenter : IInfoViewPresenter
    {
        private readonly IInfoView _infoView;

        public InfoViewPresenter(
            IInfoView infoView)
        {
            _infoView = infoView;
        }

        public void StartInfoView(string info)
        {
            _infoView.StartAnimation(new InfoViewDto(info));
        }

        public void Stop()
        {
            _infoView.StopAnimation();
        }
    }
}