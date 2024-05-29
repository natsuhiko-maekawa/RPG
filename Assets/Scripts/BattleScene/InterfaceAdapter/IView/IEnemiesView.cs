using System.Collections.Generic;
using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.Presenter.AilmentsView;
using BattleScene.InterfaceAdapter.Presenter.CharacterVibesView;
using BattleScene.InterfaceAdapter.Presenter.DigitView;
using BattleScene.InterfaceAdapter.Presenter.EnemyView;
using BattleScene.InterfaceAdapter.Presenter.FrameView;
using BattleScene.InterfaceAdapter.Presenter.StatusBarView;

namespace BattleScene.InterfaceAdapter.IView
{
    public interface IEnemiesView
    {
        public Task StartEnemyView(List<EnemyViewDto> dtoList);
        public Task StartEnemyAilmentsView(EnemyAilmentsViewDto dto);
        public Task StartEnemyDigitView(EnemyDigitViewDto dto);
        public Task StartEnemyFrameView(EnemyFrameViewDto dto);
        public void StopEnemyFrameView();
        public Task StartEnemyHpBarView(EnemyHpBarViewDto dto);
        public Task StartEnemyVibesView(EnemyVibesViewDto dto);
    }
}