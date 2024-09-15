using System;
using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.Presenter.AilmentsView;
using BattleScene.InterfaceAdapter.Presenter.CharacterVibesView;
using BattleScene.InterfaceAdapter.Presenter.FrameView;

namespace BattleScene.InterfaceAdapter.Interface
{
    public interface IEnemiesView
    {
        [Obsolete]
        public Task StartEnemyAilmentsView(EnemyAilmentsViewDto dto);
        [Obsolete]
        public Task StartEnemyFrameView(EnemyFrameViewDto dto);
        [Obsolete]
        public void StopEnemyFrameView();
        [Obsolete]
        public Task StartEnemyVibesView(EnemyVibesViewDto dto);
        public IEnemyView this[int i] { get; }
    }
}