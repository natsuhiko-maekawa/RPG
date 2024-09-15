using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.Presenter.DigitView;
using BattleScene.InterfaceAdapter.Presenter.FrameView;
using BattleScene.InterfaceAdapter.Presenter.PlayerView;
using BattleScene.InterfaceAdapter.Presenter.StatusBarView;

namespace BattleScene.InterfaceAdapter.Interface
{
    public interface IPlayerView
    {
        public Task StartAnimation(PlayerViewDto dto);
        public Task StartPlayerDigitView(PlayerDigitViewDto dto);
        public Task StartFrameView(FrameViewDto dto);
        public void StopPlayerFrameView();
        public Task StartPlayerHpBarView(PlayerHpBarViewDto dto);
        public Task StartPlayerTpBarView(PlayerTpBarViewDto dto);
        public Task StartTechnicalPointBarView(TechnicalPointBarViewDto dto);
        public Task StartPlayerVibesView();
    }
}