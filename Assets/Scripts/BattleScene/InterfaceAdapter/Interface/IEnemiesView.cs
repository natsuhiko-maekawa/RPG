using System;
using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.Presenter.AilmentsView;
using BattleScene.InterfaceAdapter.Presenter.CharacterVibesView;
using BattleScene.InterfaceAdapter.Presenter.DigitView;
using BattleScene.InterfaceAdapter.Presenter.EnemyView;
using BattleScene.InterfaceAdapter.Presenter.FrameView;
using BattleScene.InterfaceAdapter.Presenter.StatusBarView;

namespace BattleScene.InterfaceAdapter.Interface
{
    public interface IEnemiesView
    {
        public Task StartEnemyView(EnemyViewDto dto);
        [Obsolete]
        public Task StartEnemyAilmentsView(EnemyAilmentsViewDto dto);
        [Obsolete]
        public Task StartEnemyDigitView(EnemyDigitViewDto dto);
        [Obsolete]
        public Task StartEnemyFrameView(EnemyFrameViewDto dto);
        [Obsolete]
        public void StopEnemyFrameView();
        [Obsolete]
        public Task StartEnemyHpBarView(EnemyHpBarViewDto dto);
        [Obsolete]
        public Task StartEnemyVibesView(EnemyVibesViewDto dto);
        public IEnemyView this[int i] { get; }
    }
}