using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.Presenter.InfoView;

namespace BattleScene.InterfaceAdapter.Interface
{
    public interface IInfoView
    {
        public Task StartAnimation(InfoViewDto dto);
        public void StopAnimation();
    }
}