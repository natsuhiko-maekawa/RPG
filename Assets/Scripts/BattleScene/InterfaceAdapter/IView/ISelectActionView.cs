using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.Presenter.SelectActionView;

namespace BattleScene.InterfaceAdapter.IView
{
    public interface ISelectActionView
    {
        public Task StartAnimation(SelectActionViewDto selectActionViewDto);
        public void StopAnimation();
    }
}