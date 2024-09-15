using System;
using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.Presenter.FrameView;

namespace BattleScene.InterfaceAdapter.Interface
{
    public interface IEnemiesView
    {
        [Obsolete]
        public Task StartEnemyFrameView(EnemyFrameViewDto dto);
        [Obsolete]
        public void StopEnemyFrameView();
        public IEnemyView this[int i] { get; }
    }
}