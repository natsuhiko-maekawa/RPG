using System.Threading.Tasks;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.Facade
{
    public class PlayerSelectActionOutput
    {
        private readonly InfoViewPresenter _infoView;
        private readonly GridViewPresenter _gridView;

        public PlayerSelectActionOutput(
            InfoViewPresenter infoView,
            GridViewPresenter gridView)
        {
            _infoView = infoView;
            _gridView = gridView;
        }

        public async Task StartAsync()
        {
            _infoView.StartAnimationAsync(MessageCode.VerticalSelect);
            var gridAnimation = _gridView.StartAnimationAsync();
            await Task.WhenAll(gridAnimation);
        }

        public void Stop()
        {
            _infoView.StopAnimation();
            _gridView.Stop();
        }
    }
}