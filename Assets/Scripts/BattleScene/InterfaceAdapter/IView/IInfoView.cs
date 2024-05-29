using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.Presenter.InfoView;

namespace BattleScene.InterfaceAdapter.IView
{
    public interface IInfoView
    {
        public Task StartAnimation(InfoViewDto dto);
        public void StopAnimation();
    }
}