using BattleScene.Framework.ViewModel;
using BattleScene.UseCases.View.InfoView.OutputBoundary;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class InfoViewPresenter : IInfoViewPresenter
    {
        private readonly Framework.View.InfoView _infoView;

        public InfoViewPresenter(
            Framework.View.InfoView infoView)
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