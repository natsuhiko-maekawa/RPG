using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.Presenter.AilmentsView;
using BattleScene.InterfaceAdapter.Presenter.CharacterVibesView;
using BattleScene.InterfaceAdapter.Presenter.DigitView;
using BattleScene.InterfaceAdapter.Presenter.EnemyView;
using BattleScene.InterfaceAdapter.Presenter.FrameView;
using BattleScene.InterfaceAdapter.Presenter.StatusBarView;

namespace BattleScene.InterfaceAdapter.Interface
{
    public interface IEnemyView
    {
        public void SetActive(bool value);
        public Task SetImage(EnemyViewDto dto);
        public Task StartAilmentAnimationAsync(EnemyAilmentsViewDto dto);
        public Task StartDigitAnimationAsync(EnemyDigitViewDto dto);
        public Task StartFrameAnimationAsync(FrameViewDto dto);
        public void StopFrameAnimation();
        public Task StartHitPointBarAnimationAsync(EnemyHpBarViewDto dto);
        public Task StartVibesAnimationAsync(EnemyVibesViewDto dto);
    }
}