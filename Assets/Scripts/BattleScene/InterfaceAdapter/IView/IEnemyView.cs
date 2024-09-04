using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.Presenter.AilmentsView;
using BattleScene.InterfaceAdapter.Presenter.CharacterVibesView;
using BattleScene.InterfaceAdapter.Presenter.DigitView;
using BattleScene.InterfaceAdapter.Presenter.FrameView;
using BattleScene.InterfaceAdapter.Presenter.StatusBarView;

namespace BattleScene.InterfaceAdapter.IView
{
    public interface IEnemyView
    {
        public Task StartAilmentView(EnemyAilmentsViewDto dto);
        public Task StartDigitView(EnemyDigitViewDto dto);
        public Task StartFrameView(FrameViewDto dto);
        public void StopFrameView();
        public Task StartHitPointBarView(EnemyHpBarViewDto dto);
        public Task StartVibesView(EnemyVibesViewDto dto);
    }
}